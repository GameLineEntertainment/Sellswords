// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32929,y:32679,varname:node_4795,prsc:2|emission-6757-OUT,alpha-4728-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32042,y:32567,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b80c73badf90597449a2cf4eab88e541,ntxv:2,isnm:False|UVIN-8138-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32326,y:32740,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31862,y:32748,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31862,y:32924,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32150,y:32908,cmnt:Speed,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32229,y:33006,varname:node_798,prsc:2|A-2053-A,B-797-A;n:type:ShaderForge.SFN_Slider,id:5956,x:32049,y:33208,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_5956,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3931624,max:1;n:type:ShaderForge.SFN_Multiply,id:9234,x:32443,y:32942,varname:node_9234,prsc:2|A-798-OUT,B-5956-OUT,C-6074-A;n:type:ShaderForge.SFN_Multiply,id:6757,x:32518,y:32625,varname:node_6757,prsc:2|A-6074-RGB,B-2393-OUT;n:type:ShaderForge.SFN_TexCoord,id:5263,x:30909,y:32318,varname:node_5263,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Rotator,id:8138,x:31787,y:32567,varname:node_8138,prsc:2|UVIN-5263-UVOUT,SPD-5209-OUT;n:type:ShaderForge.SFN_RemapRange,id:2457,x:31323,y:33144,varname:node_2457,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5263-UVOUT;n:type:ShaderForge.SFN_Length,id:3617,x:31570,y:33205,varname:node_3617,prsc:2|IN-2457-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3745,x:31867,y:33611,varname:node_3745,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8320-OUT;n:type:ShaderForge.SFN_Floor,id:8320,x:31641,y:33548,varname:node_8320,prsc:2|IN-8685-OUT;n:type:ShaderForge.SFN_OneMinus,id:444,x:32199,y:33604,varname:node_444,prsc:2|IN-3745-OUT;n:type:ShaderForge.SFN_RemapRange,id:3499,x:31216,y:33381,varname:node_3499,prsc:2,frmn:0,frmx:1,tomn:-3,tomx:3|IN-5263-UVOUT;n:type:ShaderForge.SFN_Length,id:8685,x:31430,y:33467,varname:node_8685,prsc:2|IN-3499-OUT;n:type:ShaderForge.SFN_Add,id:8616,x:32305,y:33351,varname:node_8616,prsc:2|A-3617-OUT,B-4549-OUT;n:type:ShaderForge.SFN_Multiply,id:4728,x:32660,y:33068,varname:node_4728,prsc:2|A-5956-OUT,B-9234-OUT,C-6215-OUT;n:type:ShaderForge.SFN_Slider,id:5209,x:31429,y:32691,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5209,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:4549,x:31742,y:33405,ptovrint:False,ptlb:Area,ptin:_Area,varname:node_4549,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:4,cur:-0.1025641,max:-1;n:type:ShaderForge.SFN_ArcTan,id:4972,x:32718,y:33505,varname:node_4972,prsc:2|IN-8616-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:6215,x:32489,y:33275,varname:node_6215,prsc:2,a:2,b:0|IN-8616-OUT;proporder:6074-797-5956-5209-4549;pass:END;sub:END;*/

Shader "GLS_Shaders/AbyssFog" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Opacity ("Opacity", Range(0, 1)) = 0.3931624
        _Speed ("Speed", Range(0, 1)) = 0
        _Area ("Area", Range(4, -1)) = -0.1025641
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
            uniform float _Speed;
            uniform float _Area;
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
                float4 node_2229 = _Time;
                float node_8138_ang = node_2229.g;
                float node_8138_spd = _Speed;
                float node_8138_cos = cos(node_8138_spd*node_8138_ang);
                float node_8138_sin = sin(node_8138_spd*node_8138_ang);
                float2 node_8138_piv = float2(0.5,0.5);
                float2 node_8138 = (mul(i.uv0-node_8138_piv,float2x2( node_8138_cos, -node_8138_sin, node_8138_sin, node_8138_cos))+node_8138_piv);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8138, _MainTex));
                float3 emissive = (_MainTex_var.rgb*(_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*2.0));
                float3 finalColor = emissive;
                float node_8616 = (length((i.uv0*2.0+-1.0))+_Area);
                fixed4 finalRGBA = fixed4(finalColor,(_Opacity*((i.vertexColor.a*_TintColor.a)*_Opacity*_MainTex_var.a)*lerp(2,0,node_8616)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
