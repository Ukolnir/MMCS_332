#version 330 core
layout(location = 0) in vec3 coord;
layout(location = 1) in vec3 normal;
void main() {
	gl_Position = vec4(coord, 1.0);
}