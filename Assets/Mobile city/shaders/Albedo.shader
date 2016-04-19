Shader "skybox/albedo"
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags {"LightMode" = "Always" "Queue"="Geometry"}
		
		Fog {Mode Off}

		Pass
		{
			Name "BASE"
			Lighting Off


			SetTexture [_MainTex] 
			{
				constantColor [_Color]
				combine texture * constant
			}
		}
	} 
}
