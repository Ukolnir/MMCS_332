attribute vec3 coord;
uniform mat4 matrix;
uniform mat4 MVP;
void main() {
	gl_Position = MVP * matrix * vec4(coord, 1.0);
}  