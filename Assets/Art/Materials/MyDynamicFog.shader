// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32929,y:32679,varname:node_4795,prsc:2|emission-6757-OUT,alpha-9234-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32039,y:32595,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b80c73badf90597449a2cf4eab88e541,ntxv:2,isnm:False|UVIN-8138-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32372,y:32810,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31937,y:32780,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31788,y:33026,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:31788,y:32930,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32250,y:32997,varname:node_798,prsc:2|A-6074-A,B-2053-A,C-797-A;n:type:ShaderForge.SFN_Slider,id:5956,x:32172,y:33237,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_5956,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:9234,x:32564,y:32970,varname:node_9234,prsc:2|A-798-OUT,B-5956-OUT;n:type:ShaderForge.SFN_Multiply,id:6757,x:32564,y:32757,varname:node_6757,prsc:2|A-6074-RGB,B-2393-OUT;n:type:ShaderForge.SFN_TexCoord,id:5263,x:31623,y:32501,varname:node_5263,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:5756,x:31623,y:32717,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_5756,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Rotator,id:8138,x:31823,y:32595,varname:node_8138,prsc:2|UVIN-5263-UVOUT,SPD-5756-OUT;proporder:6074-797-5956-5756;pass:END;sub:END;*/

Shader "GLS_Shaders/DynamicFog" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Value ("Value", Float ) = 0.15
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Opacity;
            uniform float _Value;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_9264 = _Time;
                float node_8138_ang = node_9264.g;
                float node_8138_spd = _Value;
                float node_8138_cos = cos(node_8138_spd*node_8138_ang);
                float node_8138_sin = sin(node_8138_spd*node_8138_ang);
                float2 node_8138_piv = float2(0.5,0.5);
                float2 node_8138 = (mul(i.uv0-node_8138_piv,float2x2( node_8138_cos, -node_8138_sin, node_8138_sin, node_8138_cos))+node_8138_piv);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8138, _MainTex));
                float3 emissive = (_MainTex_var.rgb*(_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*2.0));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,((_MainTex_var.a*i.vertexColor.a*_TintColor.a)*_Opacity));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
