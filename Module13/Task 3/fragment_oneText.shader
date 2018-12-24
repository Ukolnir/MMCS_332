#version 330 core
in vec2 UV;
uniform sampler2D myTextureSampler;
uniform int use;
in vec4 FragmentColor;
void main() {
	if (use != 0)
		gl_FragColor = FragmentColor * texture(myTextureSampler, UV);
	else
		gl_FragColor = FragmentColor;
}