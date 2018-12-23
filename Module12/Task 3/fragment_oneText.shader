#version 330 core
in vec2 UV;
uniform sampler2D myTextureSampler;
void main() {
	gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0) + texture(myTextureSampler, UV);
}