#version 330 core
layout(location = 0) in vec3 coord;
layout(location = 1) in vec2 vertexUV;
out vec2 UV;
uniform mat4 matrix;
void main() {
	gl_Position = matrix * vec4(coord, 1.0);
	UV = vertexUV;
}