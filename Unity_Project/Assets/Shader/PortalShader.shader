Shader "Unlit/StrangerPortalShader"
{
    Properties
    {
        [Header(Base Appearance)]
        _ColorCore ("Core Color (Deep Red)", Color) = (0.3, 0.0, 0.0, 1)
        _ColorRim ("Rim Color (Glowing Orange)", Color) = (1.0, 0.3, 0.1, 1)
        
        [Header(Shape and Noise)]
        _Radius ("Portal Radius", Range(0.1, 3.0)) = 1.0
        _DisplaceStrength ("Fleshiness Strength", Range(0.0, 2.0)) = 0.8
        _NoiseScale ("Noise Scale", Range(0.1, 5.0)) = 1.5
        _AnimationSpeed ("Writhing Speed", Range(0.0, 3.0)) = 0.5
        
        [Header(Rendering)]
        _RimPower ("Glow Sharpness", Range(1.0, 8.0)) = 3.0
        _Transparency ("Edge Transparency", Range(0.0, 1.0)) = 0.1
        _MaxDistance ("Raymarch Max Distance", Range(1, 50)) = 20

    }
    SubShader
    {
        // Configuration pour la transparence
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        ZWrite Off // Important pour les effets transparents complexes
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // --- Paramètres ---
            float4 _ColorCore;
            float4 _ColorRim;
            float _Radius;
            float _DisplaceStrength;
            float _NoiseScale;
            float _AnimationSpeed;
            float _RimPower;
            float _Transparency;
            float _MaxDistance;

            struct appdata { float4 vertex : POSITION; };
            struct v2f {
                float4 pos : SV_POSITION;
                float3 wPos : TEXCOORD0; // World Position
            };


            // Fonction simple pour une sphère
            float sdfSphere(float3 p, float r) { return length(p) - r; }

            float fleshyNoise(float3 p)
            {
                float t = _Time.y * _AnimationSpeed;
                float3 np = p * _NoiseScale;
                
                float n = sin(np.x + t) * cos(np.y - t*0.5) * sin(np.z + t*0.8);
                np *= 2.0; 
                n += 0.5 * (sin(np.x - t*1.2) * cos(np.y + t) * sin(np.z - t*0.4));
                return n;
            }

            // Définit la forme finale du portail
            float map(float3 p)
            {
                // On part d'une sphère de base
                float baseSphere = sdfSphere(p, _Radius);
                
                // On calcule le déplacement
                float displacement = fleshyNoise(p) * _DisplaceStrength;
                
                // On combine : la forme est la sphère PLUS le déplacement
                return baseSphere + displacement;
            }
           
            float3 calcNormal(float3 p)
            {
                float2 e = float2(0.001, 0.0); // Epsilon (toute petite distance)
                return normalize(float3(
                    map(p + e.xyy) - map(p - e.xyy),
                    map(p + e.yxy) - map(p - e.yxy),
                    map(p + e.yyx) - map(p - e.yyx)
                ));
            }

            // --- VERTEX SHADER ---
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // IMPORTANT : Pour que l'objet puisse bouger et tourner, 
                // on fait le raymarching en "Object Space" (espace local).
                // On convertit donc la position du sommet en monde pour le fragment.
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            // --- FRAGMENT SHADER (Raymarching Loop) ---
            fixed4 frag (v2f i) : SV_Target
            {
                // --- Configuration du Rayon ---
                float3 rayOriginWorld = _WorldSpaceCameraPos;
                float3 rayDirWorld = normalize(i.wPos - rayOriginWorld);

                // Conversion en Espace Objet Local pour que le portail suive le cube
                float3 rayOriginLocal = mul(unity_WorldToObject, float4(rayOriginWorld, 1.0)).xyz;
                float3 rayDirLocal = normalize(mul((float3x3)unity_WorldToObject, rayDirWorld));

                float t = 0; // Distance parcourue
                int maxSteps = 64;
                float3 currentPosLocal;

                // --- Boucle de marche ---
                for(int step = 0; step < maxSteps; step++)
                {
                    currentPosLocal = rayOriginLocal + rayDirLocal * t;
                    float d = map(currentPosLocal);

                    // --- HIT
                    if(d < 0.001) 
                    {
                        // 1. Calculer la normale de la surface touchée
                        float3 normal = calcNormal(currentPosLocal);
                        
                        // 2. Effet Fresnel (Rim Lighting)
                        // On regarde si le rayon est perpendiculaire à la normale.
                        // Si oui (sur les bords), ça brille plus fort.
                        float fresnel = pow(1.0 - saturate(dot(normal, -rayDirLocal)), _RimPower);
                        
                        // 3. Mélange des couleurs
                        float4 finalColor = lerp(_ColorCore, _ColorRim, fresnel);

                        // 4. Gestion de la transparence
                        // Plus on est au centre de la matière (fresnel faible), plus c'est opaque.
                        // Plus on est sur les bords fins, plus c'est transparent.
                        finalColor.a = saturate(fresnel + _Transparency);
                        // On s'assure que le cœur a quand même un peu d'opacité
                        finalColor.a = max(finalColor.a, 0.3); 

                        return finalColor;
                    }
                    
                    t += d; // On avance
                    
                    // Distance de sécurité : si on sort de la boîte englobante (en gros)
                    if(t > _MaxDistance) break; 
                }

                // Pas de touche : pixel transparent
                return fixed4(0,0,0,0);
            }
            ENDCG
        }
    }
}