#version 330 core
in vec2 UV;
uniform sampler2D myTextureSampler;
in vec3 fragmentColor;
void main() {
	gl_FragColor = texture(myTextureSampler, UV);
}