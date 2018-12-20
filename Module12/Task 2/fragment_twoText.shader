#version 330 core
in vec2 UV;
uniform sampler2D myTextureSampler;
uniform sampler2D myTextureSampler1;
in vec3 fragmentColor;
uniform float mix_f;
void main() {
	gl_FragColor = mix(texture(myTextureSampler, UV), texture(myTextureSampler1, UV), mix_f);
}