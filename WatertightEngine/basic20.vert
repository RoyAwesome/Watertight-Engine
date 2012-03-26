#version 120

attribute vec4 a_Vertex;
attribute vec4 a_Color;


uniform mat4 View;
uniform mat4 Proj;

varying vec4 col;


void main()
{
	gl_Position = Proj * View * a_Vertex;
	col = a_Color;

}
