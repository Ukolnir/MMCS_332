#version 330 core
in vec2 UV;
uniform sampler2D myTextureSampler;
uniform sampler2D myTextureSampler1;
in vec3 fragmentColor;
void main() {
	gl_FragColor = texture(myTextureSampler, UV) * texture(myTextureSampler1, UV);
}