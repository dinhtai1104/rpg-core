Shader "Effects/UVFlow" {
	Properties {
		[HDR] _TintColor ("主颜色", Vector) = (1,1,1,1)
		_MainTex ("主纹理", 2D) = "white" {}
		_Alpha ("透明度", Range(0, 1)) = 1
		_MainSpeed ("主纹理偏移", Vector) = (0,0,0,0)
		_MaskTex ("蒙版纹理", 2D) = "white" {}
		_MaskSpeed ("蒙版纹理偏移", Vector) = (0,0,0,0)
		[HideInInspector] _ColorMode ("ColorMode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _TwoSide ("TwoSide", Float) = 0
		[HideInInspector] _Ztest ("Ztest", Float) = 2
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//CustomEditor "CustomBaseParticleGUI"
}