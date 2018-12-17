#version 330 core
layout(location = 0) in vec3 coord;
layout(location = 1) in vec2 vertexUV;
layout(location = 2) in vec3 vertexColor;
uniform mat4 MVP;
out vec2 UV;
out vec3 fragmentColor;
void main() {
	gl_Position = MVP * vec4(coord, 1.0);
	fragmentColor = vertexColor;
	UV = vertexUV;
}