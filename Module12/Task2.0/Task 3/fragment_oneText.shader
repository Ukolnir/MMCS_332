#version 330 core
in vec2 UV;
uniform sampler2D myTextureSampler;
void main() {
	gl_FragColor = texture(myTextureSampler, UV);
}