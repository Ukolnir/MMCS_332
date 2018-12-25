#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <Windows.h>
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glew-2.1.0\include\GL\glew.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glew-2.1.0\include\GL\wglew.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut_std.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\freeglut_ext.h"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\Module10\lib\SOIL.h"
#include <string>
#include <vector>
#include <fstream>
#define GLM_ENABLE_EXPERIMENTAL
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\glm.hpp"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\gtc\matrix_transform.hpp"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\gtx\transform.hpp"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\gtc\matrix_inverse.hpp"
#include "D:\Документы\OneDrive\Документы\7 семестр\комп. графика\MMCS_332\glm\glm\gtc\type_ptr.hpp"
#include <sstream>
#include <map>
using namespace std;


//-----------------------------SHADER_MODE
//-------------------------- 0, 2 - witout texture; 1,3 - with texture.
int shader_mode = 2;

//-----------------------------TEXTURES AND MODELS

//string texPath3 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\textures\\4nqnbO1WtYI.jpg";
string texPath3 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\textures\\lotus_textures\\lotus_petal_diffuse.jpg";

string objname = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\lotus_OBJ_low.obj";


//---------------------------MOVES

float X = 0.0f, Y = 0.0f, Z = 0.0f;

float angle_X = 0.0f;
float angle_Y = 0.0f;
float angle_Z = 0.0f;


float L_X = 10.0f, L_Y = -10.0f, L_Z = -10.0f;

float L_angle_X = 0.0f;
float L_angle_Y = 0.0f;
float L_angle_Z = 0.0f;

///----------------------------------------------------------------------------
GLuint textureID;

void _LoadImage() {
	textureID = SOIL_load_OGL_texture(texPath3.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
}

GLint use = shader_mode % 2;

string vsPath = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\vertex.shader";
string fsPath = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\fragment_oneText.shader";
string vsPath1 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\vertexPhong.shader";
string fsPath1 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\fragmentPhong.shader";

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

void set_material() {
	if (use) {
		material_ambient = { 1.0, 1.0, 1.0, 1.0 };
		material_emission = { 0.0, 0.0, 0.0, 1.0 };
	}
	else {
		material_ambient = { 0.0, 0.0, 0.0, 1.0 };
		material_emission = { 1.0, 0.0, 0.0, 1.0 };
	}
	
	material_diffuse = { 0.64, 0.64, 0.64, 1.0 };
	material_specular = { 0.9, 0.9, 0.9, 1.0 };
	material_shininess = 100.0;
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
	if ((errCode = glGetError()) != GL_NO_ERROR)
		cout << "OpenGl error! - " << gluErrorString(errCode);
}

void initShader() {
	string _f;
	if (shader_mode == 2 || shader_mode == 3)
		_f = loadFile(vsPath1);
	else
		_f = loadFile(vsPath);
	const char* vsSource = _f.c_str();

	GLuint vShader, fShader;

	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	if (shader_mode == 2 || shader_mode == 3)
		_f = loadFile(fsPath1);
	else
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

void freeShader() {
	glUseProgram(0);
	glDeleteProgram(Program);
}

GLuint colorbuffer;
GLuint texturebuffer;

void loadOBJ(const std::string & path, std::vector<glm::vec3> & out_vertices, std::vector<glm::vec2> & out_uvs, std::vector<glm::vec3> & out_normals)
{
	std::vector<unsigned int> vertex_indices, uv_indices, normal_indices;
	std::vector<glm::vec3> temp_vertices;
	std::vector<glm::vec2> temp_uvs;
	std::vector<glm::vec3> temp_normals;

	std::ifstream infile(path);
	std::string line;

	int obj_scale = 1;

	while (getline(infile, line)){
		std::stringstream ss(line);
		std::string lineHeader;
		getline(ss, lineHeader, ' ');
		if (lineHeader == "v"){
			glm::vec3 vertex;
			ss >> vertex.x >> vertex.y >> vertex.z;

			vertex.x *= obj_scale;
			vertex.y *= obj_scale;
			vertex.z *= obj_scale;
			temp_vertices.push_back(vertex);
		}
		else if (lineHeader == "vt"){
			glm::vec2 uv;
			ss >> uv.x >> uv.y;
			temp_uvs.push_back(uv);
		}
		else if (lineHeader == "vn"){
			glm::vec3 normal;
			ss >> normal.x >> normal.y >> normal.z;
			temp_normals.push_back(normal);
		}
		else if (lineHeader == "f"){
			int vertex_index, uv_index, normal_index;
			char slash;
			int cnt = 0;
			while (ss >> vertex_index >> slash >> uv_index >> slash >> normal_index)
			{
				vertex_indices.push_back(vertex_index);
				uv_indices.push_back(uv_index);
				normal_indices.push_back(normal_index);
				cnt++;
			}

			if (cnt == 3)
			{
				vertex_indices.push_back(vertex_index);
				uv_indices.push_back(uv_index);
				normal_indices.push_back(normal_index); cnt++;
			}
		}
	}

	// For each vertex of each triangle
	for (unsigned int i = 0; i < vertex_indices.size(); i++){
		unsigned int vertexIndex = vertex_indices[i];
		glm::vec3 vertex = temp_vertices[vertexIndex - 1];
		out_vertices.push_back(vertex);

		unsigned int uvIndex = uv_indices[i];
		glm::vec2 uv = temp_uvs[uvIndex - 1];
		out_uvs.push_back(uv);

		unsigned int normalIndex = normal_indices[i];
		glm::vec3 normal = temp_normals[normalIndex - 1];
		out_normals.push_back(normal);
	}
}

struct PackedVertex
{
	glm::vec3 position;
	glm::vec2 uv;
	glm::vec3 normal;
	bool operator<(const PackedVertex that) const
	{
		return memcmp((void*)this, (void*)&that, sizeof(PackedVertex)) > 0;
	};
};

bool getSimilarVertexIndex_fast(
	PackedVertex & packed,
	std::map<PackedVertex, unsigned short> & VertexToOutIndex,
	unsigned short & result
){
	std::map<PackedVertex, unsigned short>::iterator it = VertexToOutIndex.find(packed);
	if (it == VertexToOutIndex.end()) return false;
	else{
		result = it->second;
		return true;
	}
}

GLuint VBO, VAO, IBO;

void indexVBO(
	std::vector<glm::vec3> & in_vertices,
	std::vector<glm::vec2> & in_uvs,
	std::vector<glm::vec3> & in_normals,

	std::vector<unsigned short> & out_indices,
	std::vector<glm::vec3> & out_vertices,
	std::vector<glm::vec2> & out_uvs,
	std::vector<glm::vec3> & out_normals
){
	std::map<PackedVertex, unsigned short> VertexToOutIndex;

	// For each input vertex
	for (unsigned int i = 0; i < in_vertices.size(); i++)
	{
		PackedVertex packed = { in_vertices[i], in_uvs[i], in_normals[i] };

		// Try to find a similar vertex in out_XXXX
		unsigned short index;
		bool found = getSimilarVertexIndex_fast(packed, VertexToOutIndex, index);

		if (found)
		{ // A similar vertex is already in the VBO, use it instead!
			out_indices.push_back(index);
		}
		else
		{ // If not, it needs to be added in the output data.
			out_vertices.push_back(in_vertices[i]);
			out_uvs.push_back(in_uvs[i]);
			out_normals.push_back(in_normals[i]);
			unsigned short newindex = (unsigned short)out_vertices.size() - 1;
			out_indices.push_back(newindex);
			VertexToOutIndex[packed] = newindex;
		}
	}
}

std::vector<glm::vec3> indexed_vertices;
std::vector<glm::vec2> indexed_uvs;
std::vector<glm::vec3> indexed_normals;
std::vector<unsigned short> indices;
GLuint vertexbuffer;
GLuint uvbuffer;
GLuint normalbuffer;
GLuint elementbuffer;

void initVBO() {
	std::vector<glm::vec3> vertices;
	std::vector<glm::vec2> uvs;
	std::vector<glm::vec3> normals;
	loadOBJ(objname.c_str(), vertices, uvs, normals);
	indexVBO(vertices, uvs, normals, indices, indexed_vertices, indexed_uvs, indexed_normals);
	
	glGenBuffers(1, &vertexbuffer);
	glGenBuffers(1, &uvbuffer);
	glGenBuffers(1, &normalbuffer);
	glGenBuffers(1, &elementbuffer);
	glGenVertexArrays(1, &VAO);

	glBindVertexArray(VAO);
	//binding
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, indexed_vertices.size() * sizeof(glm::vec3), &indexed_vertices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, uvbuffer);
	glBufferData(GL_ARRAY_BUFFER, indexed_uvs.size() * sizeof(glm::vec2), &indexed_uvs[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, normalbuffer);
	glBufferData(GL_ARRAY_BUFFER, indexed_normals.size() * sizeof(glm::vec2), &indexed_normals[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementbuffer);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned short), &indices[0], GL_STATIC_DRAW);
}

glm::mat4 Projection;
glm::vec3 eye;

void resizeWindow(int width, int height) { 
	glViewport(0, 0, width, height); 

	Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 1000.0f);
	eye = { 4, 7, -5 };
	glm::vec3 center = { 0,0,0 };
	glm::vec3 up = { 0,1,0 };

	Projection *= glm::lookAt(eye, center, up);
}

void render1() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	
	glm::mat4  m = glm::rotate(glm::mat4(1.0f), L_angle_X, glm::vec3(1, 0,0));
    m = glm::rotate(m, L_angle_Y, glm::vec3(0, 1, 0));
	m = glm::rotate(m, L_angle_Z, glm::vec3(0, 0, 1));
	light_position = m * light_position;

	glm::mat4 Model;

	Model = glm::translate(glm::mat4(1.0f), glm::vec3(X, Y, Z));
	Model = glm::rotate(Model, angle_X, glm::vec3(1.0f, 0.0f, 0.0f));
	Model = glm::rotate(Model, angle_Y, glm::vec3(0.0f, 1.0f, 0.0f));
	Model = glm::rotate(Model, angle_Z, glm::vec3(0.0f, 0.0f, 1.0f));

	glm::mat3 transform_normal = glm::inverseTranspose(glm::mat3(Model));

	set_light();
	set_material();

	glUseProgram(Program);

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

	glUniform1i(glGetUniformLocation(Program, "use"),use);

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
	glBindTexture(GL_TEXTURE_2D, textureID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

	glUniform1i(glGetUniformLocation(Program,"myTextureSampler"), 0);

	glBindVertexArray(VAO);
	glDrawElements(GL_QUADS, indices.size(), GL_UNSIGNED_SHORT, 0);
	//GL_QUADS   GL_TRIANGLES
	glDisableVertexAttribArray(0);

	glUseProgram(0);

	glFlush();

	checkOpenGLerror();

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
	case GLUT_KEY_ALT_R:
		angle_X += 0.2;
		break;
	case GLUT_KEY_ALT_L:
		angle_X -= 0.2;
		break;
	case GLUT_KEY_CTRL_L:
		angle_Y += 0.2;
		break;
	case GLUT_KEY_CTRL_R:
		angle_Y -= 0.2;
		break;
	case GLUT_KEY_SHIFT_L:
		angle_Z += 0.2;
		break;
	case GLUT_KEY_SHIFT_R:
		angle_Z -= 0.2;
		break;
	case GLUT_KEY_F1:
		X += 2;
		break;
	case GLUT_KEY_F2:
		X -= 2;
		break;
	case GLUT_KEY_F3:
		Y += 2;
		break;
	case GLUT_KEY_F4:
		Y -= 2;
		break;
	case GLUT_KEY_F5:
		Z += 2;
		break;
	case GLUT_KEY_F6:
		Z -= 2;
		break;

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
	glClearColor(0, 0, 0, 0);
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);

	initShader();

	_LoadImage();

	initVBO();

	glutReshapeFunc(resizeWindow);
	glutSpecialFunc(specialKeys);
	glutDisplayFunc(render1);
	//glutKeyboardFunc(keyboard);
	glutMainLoop();

	freeShader();
}