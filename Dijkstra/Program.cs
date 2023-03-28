using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

class MakeGraph
{
    int input;
    //int inputV;
    int inputVM; //버텍스 갯수

    public int[,] graph; //다차원 배열 MAP
    public int[] vertex; //버텍스 갯수
    public bool[,] isLinked; //이어져 있는지
    public int[] Vertex;//최소 이동 저장 배열
    public bool[] sucess;//최소인가?
    public string[] vertexs; //버텍스 이름

    //
    public string[] gyenro;

    //자동 생성 버텍스 네이밍
    string[] name = new string[20] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T"};

    public MakeGraph(){}

    //최솟값 초기화
    public void InitialVertex()
    {
        //저장된 최솟거리 - 0 으로 초기화
        Array.Fill(Vertex, 0);
    }
    //그래프 입력 생성
    public int[,] MakeGraphM()
    {
        Console.Write("정점 갯수 : ");
        inputVM = int.Parse(Console.ReadLine());
        Console.WriteLine();

        graph = new int[inputVM, inputVM];        //다차원 배열 MAP
        vertex = new int[inputVM];               //버텍스 갯수
        isLinked = new bool[inputVM, inputVM];  //이어져 있는지
        Vertex = new int[inputVM];             //최소 이동 저장 배열
        sucess = new bool[inputVM];           //최소인가?
        vertexs = new string[inputVM];

        //
        gyenro = new string[inputVM];

        Array.Fill(Vertex, 0);
        Array.Fill(sucess, false);//버텍스 이름
        Array.Fill(gyenro, "a");

        for (int i = 0; i < inputVM; i++)
        {
            Console.Write($"{i+1}번 정점 이름 : ");
            vertexs[i] = Console.ReadLine();
            Console.WriteLine();
        }

        for (int i = 0; i < inputVM; i++) 
        {
            for (int j = 0; j < inputVM; j++) 
            {
                if (i != j) {
                    //이동이 가능한지
                    Console.WriteLine($"{vertexs[i]} 정점에서 {vertexs[j]} 정점으로 이동이 가능 합니까?");
                    Console.WriteLine("YES -> 1: ");
                    Console.WriteLine("NO  -> 2: ");
                    input = int.Parse(Console.ReadLine());

                if (input == 1)
                    {
                        //이동이 가능 하다면 가중치 저장
                        Console.Write($"{vertexs[i]} -> {vertexs[j]}의 가중치 : ");
                        graph[j, i] = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                    }
                    else if (input == 2)
                    {
                        //이동이 불가능 하다면 -1 로 이동 불가 저장
                        graph[j, i] = -1;
                        Console.WriteLine();
                    }
                }
                else if(i==j)
                {
                    //버텍스 영역이라면 -1로 표시 
                    graph[j, i] = -1;
                    Console.WriteLine();
                }
            }
        }
        //그래프 그리기

        //for (int i = 0; i < inputVM; i++)
        //{
            
        //    for (int j = 0; j < inputVM; j++)
        //    {
        //        Console.Write($"[{graph[i,j]}]");
        //    }
        //    if ((i + 1) / inputVM == 0) { Console.WriteLine(); }
        //}

        //다차원 배열 리턴
        return graph;
    }
    //그래프 자동 생성
    public int[,] MakeGraphRandomM()
    {
        Random random = new Random();
        int randNum;
        inputVM = random.Next(10, 21);
        Console.WriteLine();

        graph = new int[inputVM, inputVM];        //다차원 배열 MAP
        vertex = new int[inputVM];               //버텍스 갯수
        isLinked = new bool[inputVM, inputVM];  //이어져 있는지
        Vertex = new int[inputVM];             //최소 이동 저장 배열
        sucess = new bool[inputVM];           //최소인가?
        vertexs = new string[inputVM];

        Array.Fill(Vertex, 0);
        Array.Fill(sucess, false);//버텍스 이름

        for (int i = 0; i < inputVM; i++)
        {
            vertexs[i] = name[i];
        }

        for (int i = 0; i < inputVM; i++)
        {
            for (int j = 0; j < inputVM; j++)
            {
                randNum = random.Next(0,2);
                if (i != j)
                {
                    //이동이 가능한지
                    if (randNum == 0)
                    {
                        
                        //이동이 가능 하다면 가중치 저장
                        int inputNumber = random.Next(1, 10);
                        graph[j, i] = inputNumber;
                        graph[i, j] = inputNumber;
                    }
                    else if (randNum == 1)
                    {
                        //이동이 불가능 하다면 -1 로 이동 불가 저장
                        graph[j, i] = -1;
                    }
                }
                else if (i == j)
                {
                    //버텍스 영역이라면 -1로 표시 
                    graph[j, i] = -1;
                }
            }
        }

        Console.WriteLine($"정점의 갯수 : {inputVM} 개");

        //그래프 그리기
        for (int i = 0; i < inputVM; i++)
        {

            for (int j = 0; j < inputVM; j++)
            {
                //Console.Write("[{0, -6}]", array[k]);
                Console.Write("[{0,2}]", graph[i,j]);
            }
            if ((i + 1) / inputVM == 0) { Console.WriteLine(); }
        }

        Console.WriteLine();

        //다차원 배열 리턴
        return graph;
    }
    //다익스트라
    public void DaiFor(int[,] graph,int start)
    {
        gyenro = new string[inputVM];

        Console.WriteLine();

        //입력 시작 지점 부터 
        for (int i = start-1 ; i < inputVM; i++)
        {
            //이동 거리 저장
            int com = 0;
            for (int j = 0; j < inputVM; j++) 
            {
                // 이어진 길이면
                if (graph[j, i] != -1)
                {
                    //이동 한 거리 저장 
                    com = graph[j, i];

                    //지금 받은 거리가 버텍스에 저장된 거리보다 작다면 && 저장이 없다면 -> 지금 받은 거리 저장 
                    if ((Vertex[j] > Vertex[i] + com) || Vertex[j] == 0)
                    {
                        //지금 상ㅌㅐ
                        Console.WriteLine($"{vertexs[j]} -> UnSucess");

                        //지금 거리 버텍스에 저장
                        Vertex[j] = Vertex[i] + com;

                        gyenro[i] = vertexs[i];

                        //저장된 거리 표시
                        Console.WriteLine($"{vertexs[j]} -> {Vertex[j]}");

                        Console.WriteLine($"{vertexs[j]} -> Sucess");
                    }

                    //지금 받은 거리가 기존 거리보다 크다면
                    else
                    {
                        gyenro[i] = "a";
                        //버텍스 상태 완료
                        sucess[j] = true;
                        //Console.WriteLine($"{vertexs[j]} -> Sucess");ㅋ
                        continue;
                    }

                    //아직 완료 되지 않았고 버텍스에 값이 있다면
                    if (sucess[j] == false && Vertex[j] != 0)
                    {
                        //재귀
                        DaiFor(graph,start);
                    }
                }
            }
        } 
        for(int i = 0;i< inputVM;i++)
        {
            Console.Write(gyenro[i]);
        }
    }
}

//메인
namespace A009_Dijkstra
{
    class Program
    {
        
        static void Main()
        {
            int[,] d;
            int start;
            int end;
                
            MakeGraph m = new MakeGraph();

            Console.WriteLine("1 -> 자동 생성");
            Console.WriteLine("2 -> 입력 생성");
            int input = int.Parse(Console.ReadLine());

            //자동 생성
            if (input == 1)
            {
                d = m.MakeGraphRandomM();
                while (true)
                {
                    Console.Write("시작 버텍스 : ");
                    start = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("도착 버텍스 : ");
                    end = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    //start 부터 다익스트라
                    m.DaiFor(d, start);

                    Console.WriteLine($"{m.vertexs[start - 1]} 부터 {m.vertexs[end - 1]} 까지 최소이동 : {m.Vertex[end - 1]}");

                    //이동 할 수 없는 거리면
                    if (m.Vertex[end - 1] == 0)
                    {
                        Console.Write("이동할 수 없음");
                        Console.WriteLine();
                    }

                    Console.WriteLine("1 -> 제검색");
                    Console.WriteLine("2 -> 종료");
                    input = int.Parse(Console.ReadLine());

                    if (input == 2)
                        break;
                    //저장된 최소거리 초기화
                    else
                    {
                        m.InitialVertex();
                        continue;
                    }
                }
            }
            else
            {
                    d = m.MakeGraphM();

                    while (true)
                    {

                    Console.Write("시작 인덱스 : ");
                    start = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("도착 버텍스 : ");
                    end = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    m.DaiFor(d, start);

                    Console.WriteLine($"{m.vertexs[start - 1]} 부터 {m.vertexs[end - 1]} 까지 최소이동 : {m.Vertex[end - 1]}");
                    if (m.Vertex[end - 1] == 0)
                    {
                        Console.Write("이동할 수 없음");
                        Console.WriteLine();
                    }

                    Console.WriteLine("1 -> 제검색");
                    Console.WriteLine("2 -> 종료");
                    input = int.Parse(Console.ReadLine());

                    if (input == 2)
                        break;
                    else
                    {
                        m.InitialVertex();
                        continue;
                    }
                }
            }
        }
    }
}