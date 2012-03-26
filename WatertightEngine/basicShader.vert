#version 330


layout(location = 0) in vec4 a_Vertex;
layout(location = 1) in vec4 a_Color;

uniform mat4 View;
uniform mat4 Proj;

out vec4 col;


void main()
{
	gl_Position = Proj * View * a_Vertex;
	col = a_Color;

}
