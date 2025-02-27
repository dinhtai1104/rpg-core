Shader "Effects/Dissolve" {
	Properties {
		[HDR] _TintColor ("主颜色", Vector) = (1,1,1,1)
		_MainTex ("主纹理", 2D) = "white" {}
		_BaseMaskMap ("蒙版纹理", 2D) = "white" {}
		_NoiseMap ("溶解纹理", 2D) = "white" {}
		[HDR] _EdgeColor ("溶解边缘颜色", Vector) = (1,1,1,1)
		_Diss ("溶解阈值", Range(0, 1)) = 1
		_Soft ("边缘软硬", Range(0.5, 20)) = 0.5
		_Widge ("边缘宽度", Range(0, 10)) = 0
		_NoiseSpeed ("溶解纹理偏移", Vector) = (0,0,0,0)
		_Alpha ("透明度", Range(0, 1)) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
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