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

//-----------------------------TEXTURES AND MODELS

//string texPath3 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Module13\\Task 3\\textures\\4nqnbO1WtYI.jpg";
//---------------------------MOVES

float X = 0.0f, Y = 0.0f, Z = 0.0f;

float angle_X = 0.0f;
float angle_Y = 0.0f;
float angle_Z = 0.0f;


float L_X = 1.4f, L_Y = 15.0f, L_Z = 0.8f;

float L_angle_X = 0.0f;
float L_angle_Y = 0.0f;
float L_angle_Z = 0.0f;

///----------------------------------------------------------------------------
GLuint textureID;

void _LoadImage(string texPath3) {
	textureID = SOIL_load_OGL_texture(texPath3.c_str(), SOIL_LOAD_AUTO, SOIL_CREATE_NEW_ID, SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT);
}

GLuint Program1, Program2;
string vsPath1 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\vertexPhong.shader";
string fsPath1 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\fragmentPhong.shader";
string vsPath2 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\vertex.shader";
string fsPath2 = "D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\fragmentPhong1.shader";

int w, h;

struct  light
{
	glm::vec4 light_position, light_ambient, light_diffuse, light_specular;
	glm::vec3 light_attenuation;
	light(float X1, float Y1, float Z1)
	{
		light_position = { X1, Y1, Z1 , 1 };
		light_ambient = { 1,1,1,1 };
		light_diffuse = { 1,1,1,1 };
		light_specular = { 1,1,1,1 };
		light_attenuation = { 0,0,0 };
	}
};

struct material
{
	glm::vec4 material_ambient, material_diffuse, material_specular;
	glm::vec4 material_emission;
	float material_shininess;

	material(int am)
	{
		material_ambient = { 1.0, 1.0, 1.0, 1.0 };
		material_emission = { 0.0, 0.0, 0.0, 1.0 };

		material_diffuse = { 0.64, 0.64, 0.64, 1.0 };
		material_specular = { 0.9, 0.9, 0.9, 1.0 };
		material_shininess = 100.0;
	}
};

string loadFile(string path) {
	ifstream fs(path, ios::in);
	if (!fs) cerr << "Cannot open " << path << endl;
	string fsS;
	while (getline(fs, fsS, '\0'))
		cout << fsS << endl;
	return fsS;
}

void checkOpenGLerror(string s){
	GLenum errCode;
	setlocale(LC_ALL, "Russian");
	if ((errCode = glGetError()) != GL_NO_ERROR)
		cout << s + "  " + "OpenGl error! - " << gluErrorString(errCode) << endl;
}

void initShader(string vsPath, string fsPath, GLuint & Program) {
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

	checkOpenGLerror("initShader");
}

void freeShader(GLuint & Program) {
	glUseProgram(0);
	glDeleteProgram(Program);
}


void loadOBJ(const std::string & path, std::vector<glm::vec3> & out_vertices, std::vector<glm::vec2> & out_uvs,
	std::vector<glm::vec3> & out_normals, float obj_scale, glm::vec3 shift)
{
	std::vector<unsigned int> vertex_indices, uv_indices, normal_indices;
	std::vector<glm::vec3> temp_vertices;
	std::vector<glm::vec2> temp_uvs;
	std::vector<glm::vec3> temp_normals;

	std::ifstream infile(path);
	std::string line;


	while (getline(infile, line)){
		std::stringstream ss(line);
		std::string lineHeader;
		getline(ss, lineHeader, ' ');
		if (lineHeader == "v"){
			glm::vec3 vertex;
			ss >> vertex.x >> vertex.y >> vertex.z;

			vertex.x = vertex.x * obj_scale + shift.x;
			vertex.y = vertex.y * obj_scale + shift.y;
			vertex.z = vertex.z * obj_scale + shift.z;
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


int initVBO(GLuint & VBO, GLuint & VBN, GLuint & VBT, GLuint & IBO, string objname, 
	glm::vec3 shift, float obj_scale = 1)
{
	std::vector<glm::vec3> vertices;
	std::vector<glm::vec2> uvs;
	std::vector<glm::vec3> normals;
	std::vector<glm::vec3> indexed_vertices;
	std::vector<glm::vec2> indexed_uvs;
	std::vector<glm::vec3> indexed_normals;
	std::vector<unsigned short> indices;

	loadOBJ(objname.c_str(), vertices, uvs, normals, obj_scale, shift);
	indexVBO(vertices, uvs, normals, indices, indexed_vertices, indexed_uvs, indexed_normals);
	
	glGenBuffers(1, &VBO);
	glGenBuffers(1, &VBT);
	glGenBuffers(1, &VBN);
	glGenBuffers(1, &IBO);

	//binding
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, indexed_vertices.size() * sizeof(glm::vec3), &indexed_vertices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, VBT);
	glBufferData(GL_ARRAY_BUFFER, indexed_uvs.size() * sizeof(glm::vec2), &indexed_uvs[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, VBN);
	glBufferData(GL_ARRAY_BUFFER, indexed_normals.size() * sizeof(glm::vec2), &indexed_normals[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, IBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned short), &indices[0], GL_STATIC_DRAW);

	return indices.size();
}

glm::mat4 Projection;
glm::vec3 eye;

void resizeWindow(int width, int height) { 
	glViewport(0, 0, width, height); 

	Projection = glm::perspective(glm::radians(45.0f), 4.0f / 3.0f, 0.1f, 100.0f);
	eye = { 7, 5, 7 };
	glm::vec3 center = { 0,0,0 };
	glm::vec3 up = { 0,1,0 };

	Projection *= glm::lookAt(eye, center, up);
}

void render1(GLuint & Program, GLuint & VBO, GLuint & VBN, GLuint & VBT, GLuint & IBO, light l, material m, int cnt, GLint use) {
	//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glm::mat4  matr = glm::rotate(glm::mat4(1.0f), L_angle_X, glm::vec3(1, 0, 0));
    matr = glm::rotate(matr, L_angle_Y, glm::vec3(0, 1, 0));
	matr = glm::rotate(matr, L_angle_Z, glm::vec3(0, 0, 1));
	l.light_position = matr * l.light_position;

	glm::mat4 Model;

	Model = glm::translate(glm::mat4(1.0f), glm::vec3(X, Y, Z));
	Model = glm::rotate(Model, angle_X, glm::vec3(1.0f, 0.0f, 0.0f));
	Model = glm::rotate(Model, angle_Y, glm::vec3(0.0f, 1.0f, 0.0f));
	Model = glm::rotate(Model, angle_Z, glm::vec3(0.0f, 0.0f, 1.0f));

	glm::mat3 transform_normal = glm::inverseTranspose(glm::mat3(Model));

	glUseProgram(Program);

	glEnableVertexAttribArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, VBT);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ARRAY_BUFFER, VBN);
	glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, 0, (void*)0);

	glUniform1i(glGetUniformLocation(Program, "use"), use);
	
	glUniformMatrix4fv(glGetUniformLocation(Program, "transform_model"), 1, GL_FALSE, &Model[0][0]);
	glUniformMatrix4fv(glGetUniformLocation(Program, "transform_viewProjection"), 1, GL_FALSE, &Projection[0][0]);
	glUniform3fv(glGetUniformLocation(Program, "transform_viewPosition"), 1, glm::value_ptr(eye));
	glUniformMatrix3fv(glGetUniformLocation(Program, "transform_normal"), 1, GL_FALSE, &transform_normal[0][0]);

	glUniform4fv(glGetUniformLocation(Program, "light_position"), 1, glm::value_ptr(l.light_position));
	glUniform4fv(glGetUniformLocation(Program, "light_ambient"), 1, glm::value_ptr(l.light_ambient));
	glUniform4fv(glGetUniformLocation(Program, "light_diffuse"), 1, glm::value_ptr(l.light_diffuse));
	glUniform4fv(glGetUniformLocation(Program, "light_specular"), 1, glm::value_ptr(l.light_specular));
	glUniform3fv(glGetUniformLocation(Program, "light_attenuation"), 1, glm::value_ptr(l.light_attenuation));

	glUniform4fv(glGetUniformLocation(Program, "material_ambient"), 1, glm::value_ptr(m.material_ambient));
	glUniform4fv(glGetUniformLocation(Program, "material_diffuse"), 1, glm::value_ptr(m.material_diffuse));
	glUniform4fv(glGetUniformLocation(Program, "material_specular"), 1, glm::value_ptr(m.material_specular));
	glUniform1f(glGetUniformLocation(Program, "material_shininess"), m.material_shininess);
	glUniform4fv(glGetUniformLocation(Program, "material_emission"), 1, glm::value_ptr(m.material_emission));
	
	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

	glUniform1i(glGetUniformLocation(Program,"myTextureSampler"), 0);

	//glBindVertexArray(VAO);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, IBO);

	glDrawElements(GL_QUADS, cnt, GL_UNSIGNED_SHORT, 0);
	//GL_QUADS   GL_TRIANGLES
	//glDisableVertexAttribArray(0);

	glUseProgram(0);

	glFlush();

	checkOpenGLerror("end render");
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

vector<GLuint> VBO(11, 0), VAO(11,0), VBT(11,0), VBN(11,0), IBO(11,0), counts(11,0);

void loadModels()
{
	glm::vec3 v(1.5, 0, 1);

	counts[0] = initVBO(VBO[0], VBN[0], VBT[0], IBO[0],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\sofa.obj", v, 3 / 1800.0f);

	v = { 1.3, 0.6, 1 };

	counts[10] = initVBO(VBO[10], VBN[10], VBT[10], IBO[10],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\t.obj", v, 1 / 800.0f);


	v.x = 0;
	v.y = 0;
	v.z = 0;
	counts[1] = initVBO(VBO[1], VBN[1], VBT[1], IBO[1],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\Skaph.obj", v, 3 / 200.0f);

	v.x = 2.5;
	v.z = 1.7;
	counts[2] = initVBO(VBO[2], VBN[2], VBT[2], IBO[2],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\table.obj", v, 1 / 2.5f);

	v = { 0, 2.5, 0 };
	counts[3] = initVBO(VBO[3], VBN[3], VBT[3], IBO[3],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\flower.obj",
		v, 1 / 60.0f);

	v = { 2.2, 1.2, 2.9 };
	counts[4] = initVBO(VBO[4], VBN[4], VBT[4], IBO[4],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\coffeMug1_free_obj.obj",
		v, 1 / 170.0f);

	v = { 4.2, 0.5, 1 };
	counts[5] = initVBO(VBO[5], VBN[5], VBT[5], IBO[5],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\Bigmax_White_OBJ.obj",
		v, 1 / 90.0f);

	v = { -1.5, 0, -2 };
	counts[6] = initVBO(VBO[6], VBN[6], VBT[6], IBO[6],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\lamp.obj",
		v, 1 / 700.0f);


	v = { 0,0,0 };
	counts[7] = initVBO(VBO[7], VBN[7], VBT[7], IBO[7],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\grass.obj", v, 1.5);

	v = { 3,0,2 };
	counts[8] = initVBO(VBO[8], VBN[8], VBT[8], IBO[8],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\carpet.obj", v, 1 / 300.0f);

	v = { 0,0,0 };
	counts[9] = initVBO(VBO[9], VBN[9], VBT[9], IBO[9],
		"D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\objs\\wals.obj",
		v, 1.5);
}

void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	light l(L_X, L_Y, L_Z);
	material m(1);

	/*sofa*/
	m.material_shininess = 10.0f;
	m.material_specular = { 0.2, 0.2, 0.2, 1.0 };
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\5.jpg");
	render1(Program1, VBO[0], VBN[0], VBT[0], IBO[0], l, m, counts[0], 1);

	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\10.jpg");
	render1(Program1, VBO[10], VBN[10], VBT[10], IBO[10], l, m, counts[10], 1);
	
	/*шкаф*/
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\RetroRoomDivider_Diffuce.jpg");
	render1(Program1, VBO[1], VBN[1], VBT[1], IBO[1], l, m, counts[1], 1);

	/*table*/

	m.material_shininess = 200.0f;
	m.material_specular = { 0.8, 0.8, 0.8, 1.0 };
	m.material_emission  = { 0.1, 0.1, 0.2, 1.0 };
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\10.jpg");
	render1(Program1, VBO[2], VBN[2], VBT[2], IBO[2], l, m, counts[2], 1);

	/*flowers*/
	m.material_shininess = 40.0f;
	m.material_specular = { 0.3, 0.5, 0.3, 1.0 };
	m.material_emission = { 0.0, 0.0, 0.0, 1.0 };
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\1024.png");
	render1(Program1, VBO[3], VBN[3], VBT[3], IBO[3], l, m, counts[3], 1);

	/*cup*/
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\freeMug3.png");
	render1(Program1, VBO[4], VBN[4], VBT[4], IBO[4], l, m, counts[4], 1);

	/*Beymax*/
	m.material_shininess = 90.0f;
	m.material_specular = { 0.7, 0.7, 0.7, 1.0 };
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\3__2_.png");
	render1(Program1, VBO[5], VBN[5], VBT[5], IBO[5], l, m, counts[5], 1);

	/*lamp*/
	m.material_shininess = 90.0f;
	m.material_specular = { 0.7, 0.7, 0.7, 1.0 };
	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\3.jpg");
	render1(Program1, VBO[6], VBN[6], VBT[6], IBO[6], l, m, counts[6], 1);


	/*Пол*/
	m.material_shininess = 150.0f;
	m.material_specular = { 0.9, 0.9, 0.9, 1.0 };

	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\2.jpg");
	render1(Program1, VBO[7], VBN[7], VBT[7], IBO[7], l, m, counts[7], 1);

	/*ковер*/
	m.material_shininess = 1.0f;
	m.material_specular = { 0.0, 0.0, 0.0, 1.0 };

	_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\22.jpg");
	render1(Program1, VBO[8], VBN[8], VBT[8], IBO[8], l, m, counts[8], 1);

	/*стены*/
	m.material_diffuse = { 0.4, 0.3, 0.2, 1.0 };
	m.material_emission = { 0.2, 0.2, 0.2, 1.0 };
	//_LoadImage("D:\\Документы\\OneDrive\\Документы\\7 семестр\\комп. графика\\MMCS_332\\Individual3\\textures\\12.jpg");
	render1(Program2, VBO[9], VBN[9], VBT[9], IBO[9], l, m, counts[9], 0);

	glutSwapBuffers();
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

	initShader(vsPath1, fsPath1, Program1);
	initShader(vsPath1, fsPath2, Program2);
	loadModels();

	glutReshapeFunc(resizeWindow);
	glutSpecialFunc(specialKeys);
	glutDisplayFunc(render);
	//glutKeyboardFunc(keyboard);
	glutMainLoop();

//	freeShader(Program1);
//	freeShader(Program2);
}