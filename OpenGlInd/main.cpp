#include "Object.h"
using namespace std;

int object_index = 1;


//-----------------------------SHADER_MODE
int shader_mode = 3;
//-----------------------------TEXTURES AND MODELS

string texPath1 = "C:\\Users\\Ёллоне\\Desktop\\grass\\445.png";
string texPath2 = "C:\\Users\\Ёллоне\\Desktop\\grass\\42.jpg";
string texPath3 = "C:\\Users\\Ёллоне\\Desktop\\cottage\\cottage_diffuse.png";
string texPath4 = "C:\\Users\\Ёллоне\\Desktop\\grass\\43.jpg";


string objname1 = "C:\\Users\\Ёллоне\\Desktop\\e1.obj";
string objname2 = "C:\\Users\\Ёллоне\\Desktop\\grass.obj";
string objname3 = "C:\\Users\\Ёллоне\\Desktop\\cottage\\cottage_obj.obj";
string objname4 = "C:\\Users\\Ёллоне\\Desktop\\ifh1.obj";




//---------------------------MOVES

float L_X = 10.0f, L_Y = 10.0f, L_Z = -10.0f;

float L_angle_X = 0.0f;
float L_angle_Y = 0.0f;
float L_angle_Z = 0.0f;

///----------------------------------------------------------------------------
vector<Object> scene;
GLint use = shader_mode % 2;

string vsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 3\\vertexPhong.shader";
string fsPath = "C:\\Users\\Ёллоне\\Desktop\\Task 3\\fragmentPhong.shader";

int w, h;
GLuint Program;

glm::vec4 light_position, light_ambient, light_diffuse, light_specular;
glm::vec3 light_attenuation;

glm::vec4 material_ambient, material_diffuse, material_specular, material_emission;
float material_shininess;

void set_light() {

	light_position = {L_X, L_Y, L_Z ,1 };
	light_ambient = { 1,1,1,1 };
	light_diffuse = { 1,1,1,1 };
	light_specular = { 1,1,1,1 };
	light_attenuation = { 0,0,0 };
}

glm::mat4 Projection;
glm::vec3 eye;

void resizeWindow(int width, int height) {
	glViewport(0, 0, width, height);

	Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 5000.0f);
	eye = { 4, 7, -10 };
	glm::vec3 center = { 0,0,0 };
	glm::vec3 up = { 0,1,0 };

	Projection *= glm::lookAt(eye, center, up);
}

string loadFile(string path) {
	ifstream fs(path, ios::in);
	if (!fs) cerr << "Cannot open " << path << endl;
	string fsS;
	while (getline(fs, fsS, '\0'))
		cout << fsS << endl;
	return fsS;
}

void checkOpenGLerror(){
	GLenum errCode;
	setlocale(LC_ALL, "Russian");
	if ((errCode = glGetError()) != GL_NO_ERROR)
		cout << "OpenGl error! - " << gluErrorString(errCode);
}

void initShader() {
	string _f;
	_f = loadFile(vsPath);
	const char* vsSource = _f.c_str();

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	_f = loadFile(fsPath);
	const char* fsSource = _f.c_str();

	fShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fShader, 1, &fsSource, NULL);
	glCompileShader(fShader);

	//----

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);
	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok) { std::cout << "error attach shaders \n";   return; }

	checkOpenGLerror();
}

void initVBO(GLuint & vertexbuffer, GLuint & uvbuffer, GLuint & normalbuffer, 
	GLuint & elementbuffer, GLuint & VAO, Object obj) {
	glGenBuffers(1, &vertexbuffer);
	glGenBuffers(1, &uvbuffer);
	glGenBuffers(1, &normalbuffer);
	glGenBuffers(1, &elementbuffer);
	glGenVertexArrays(1, &VAO);

	glBindVertexArray(VAO);
	//binding
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, obj.indexed_vertices.size() * sizeof(glm::vec3), &obj.indexed_vertices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, uvbuffer);
	glBufferData(GL_ARRAY_BUFFER, obj.indexed_uvs.size() * sizeof(glm::vec2), &obj.indexed_uvs[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, normalbuffer);
	glBufferData(GL_ARRAY_BUFFER, obj.indexed_normals.size() * sizeof(glm::vec2), &obj.indexed_normals[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementbuffer);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, obj.indices.size() * sizeof(unsigned short), &obj.indices[0], GL_STATIC_DRAW);
}

void render1(GLuint & vertexbuffer, GLuint & uvbuffer, GLuint & normalbuffer,
	GLuint & elementbuffer, GLuint & VAO, Object obj) {

	glUseProgram(Program);

	glm::mat4 Model = obj.getModel();
	glm::mat3 transform_normal = glm::inverseTranspose(glm::mat3(Model));

	material_ambient = obj.material_ambient;
	material_emission = obj.material_emission;
	material_diffuse = obj.material_diffuse;
	material_specular = obj.material_specular;
	material_shininess = 100.0;

	glEnableVertexAttribArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glVertexAttribPointer(
		0,
		3,
		GL_FLOAT,
		GL_FALSE,
		0,
		(void*)0
	);

	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, uvbuffer);
	glVertexAttribPointer(
		1,
		2,
		GL_FLOAT,
		GL_FALSE,
		0,
		(void*)0
	);

	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ARRAY_BUFFER, normalbuffer);
	glVertexAttribPointer(
		2,
		3,
		GL_FLOAT,
		GL_FALSE,
		0,
		(void*)0
	);

		glUniform1i(glGetUniformLocation(Program, "use"), use);

		glUniformMatrix4fv(glGetUniformLocation(Program, "transform_model"), 1, GL_FALSE, &Model[0][0]);
		glUniformMatrix4fv(glGetUniformLocation(Program, "transform_viewProjection"), 1, GL_FALSE, &Projection[0][0]);
		glUniform3fv(glGetUniformLocation(Program, "transform_viewPosition"), 1, glm::value_ptr(eye));
		glUniformMatrix3fv(glGetUniformLocation(Program, "transform_normal"), 1, GL_FALSE, &transform_normal[0][0]);

		glUniform4fv(glGetUniformLocation(Program, "light_position"), 1, glm::value_ptr(light_position));
		glUniform4fv(glGetUniformLocation(Program, "light_ambient"), 1, glm::value_ptr(light_ambient));
		glUniform4fv(glGetUniformLocation(Program, "light_diffuse"), 1, glm::value_ptr(light_diffuse));
		glUniform4fv(glGetUniformLocation(Program, "light_specular"), 1, glm::value_ptr(light_specular));
		glUniform3fv(glGetUniformLocation(Program, "light_attenuation"), 1, glm::value_ptr(light_attenuation));

		glUniform4fv(glGetUniformLocation(Program, "material_ambient"), 1, glm::value_ptr(material_ambient));
		glUniform4fv(glGetUniformLocation(Program, "material_diffuse"), 1, glm::value_ptr(material_diffuse));
		glUniform4fv(glGetUniformLocation(Program, "material_specular"), 1, glm::value_ptr(material_specular));
		glUniform1f(glGetUniformLocation(Program, "material_shininess"), material_shininess);
		glUniform4fv(glGetUniformLocation(Program, "material_emission"), 1, glm::value_ptr(material_emission));

		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, obj.textureID);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

		glUniform1i(glGetUniformLocation(Program, "myTextureSampler"), 0);

		glBindVertexArray(VAO);
		glDrawElements(GL_QUADS, obj.indices.size(), GL_UNSIGNED_SHORT, 0);

		glFlush();

	glUseProgram(0);

	checkOpenGLerror();
}

void RENDER() {

	glm::mat4  m = glm::rotate(glm::mat4(1.0f), L_angle_X, glm::vec3(1, 0, 0));
	m = glm::rotate(m, L_angle_Y, glm::vec3(0, 1, 0));
	m = glm::rotate(m, L_angle_Z, glm::vec3(0, 0, 1));
	light_position = m * light_position;

	set_light();

	GLuint VBO = 0, VAO = 0, VBT = 0, VBN = 0, IBO = 0;

	for (int i = 0; i < scene.size(); ++i) {
		initVBO(VBO, VBT, VBN, IBO, VAO, scene[i]);
		render1(VBO, VBT, VBN, IBO, VAO, scene[i]);
	}

	glutSwapBuffers();
}


double gr_cos(float angle) noexcept
{
	return cos(angle / 180 * 3.1415926535);
}

double gr_sin(float angle) noexcept
{
	return sin(angle / 180 * 3.1415926535);
}

void specialKeys(int key, int x1, int y1)
{
	switch (key)
	{
		//cвет
	case GLUT_KEY_RIGHT:
		L_angle_X += 2;
		L_Y += 0.5 * gr_sin(L_angle_X);
		L_Z += 0.5 * gr_cos(L_angle_X);
		break;
	case GLUT_KEY_LEFT:
		L_angle_X += 2;
		L_Y -= 0.5 * gr_sin(L_angle_X);
		L_Z -= 0.5 * gr_cos(L_angle_X);
		break;
	case GLUT_KEY_UP:
		L_angle_X -= 2;
		L_Y += 0.5 * gr_sin(L_angle_X);
		L_Z += 0.5 * gr_cos(L_angle_X);
		break;
	case GLUT_KEY_DOWN:
		L_angle_X -= 2;
		L_Y -= 0.5 * gr_sin(L_angle_X);
		L_Z -= 0.5 * gr_cos(L_angle_X);
		break;
	case GLUT_KEY_PAGE_UP:
		L_angle_Z += 2;
		break;
	case GLUT_KEY_PAGE_DOWN:
		L_angle_Z -= 2;
		break;
	case GLUT_KEY_F7:
		L_X += 2;
		break;
	case GLUT_KEY_F8:
		L_X -= 2;
		break;
	case GLUT_KEY_F9:
		L_Y += 2;
		break;
	case GLUT_KEY_F10:
		L_Y -= 2;
		break;
	case GLUT_KEY_HOME:
		L_Z += 2;
		break;
	case GLUT_KEY_END:
		L_Z -= 2;
		break;
	}

	glutPostRedisplay();
}


int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_RGBA | GLUT_ALPHA | GLUT_DOUBLE);
	glutInitWindowSize(1000, 800);
	glutCreateWindow("Simple shaders");
	GLenum glew_status = glewInit();
	glClearColor(0, 0, 0, 1);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	initShader();

	Object grass = Object(objname2, texPath2, 10,
		2.0, 0.0, 0.6,
		0.4, 0.0, 0.2,
		{ 1.0, 1.0, 1.0, 1.0 }, { 0.34128, 0.64, 0.281118, 1.0 }, { 0.0, 0.0, 0.0, 1.0 }, { 0.0, 0.0, 0.0, 1.0 });

	Object dragon = Object(objname4, texPath4, 1,
		0.0, 1.0, -1.0,
		0.0, 0.0, 0.0,
		{ 1.0, 1.0, 1.0, 1.0 }, { 0.64, 0.64, 0.64, 1.0 }, { 0.1, 0.1, 0.1, 1.0 }, { 0.0, 0.0, 0.0, 1.0 });

	Object tree = Object(objname1, texPath1, 1,
		5.0, 1.0, 2.0,
		0.0, 0.0, 0.2,
		{ 1.0, 1.0, 1.0, 1.0 }, { 0.64, 0.64, 0.64, 1.0 }, { 0.1, 0.1, 0.1, 1.0 }, { 0.0, 0.0, 0.0, 1.0 });

	scene = vector<Object>();
	scene.push_back(grass);
	scene.push_back(dragon);
	scene.push_back(tree);
	for (int i = 0; i < scene.size(); ++i) {
		scene[i]._LoadImage();
		scene[i].initDataBuffers();
	}

	glutReshapeFunc(resizeWindow);
	glutSpecialFunc(specialKeys);
	glutDisplayFunc(RENDER);
	glutMainLoop();
}