using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    //ANOTAÇÔES PARA O FUTURO EU OU OUTRO PROGRAMADOR, ORGANIZAÇÂO BASICONA, VARIAVEIS EM CIMA
    //FUNÇÔES EM BAIXO, AS 2 "MAINS" NO CENTRO E NUNCA MINIMIZADS


    //CHESSMANS È UM VETOR DE PEÇADEFAULT
    public List<Moves> alfa = new List<Moves>();
    public List<Moves> beta = new List<Moves>();


    public bool isProblema = false;

    public bool capturedKing = false;
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    //matriz de objetos peçadefault(peças)
    public PeçaDefault[,] Chessmans { set; get; }
    public int PrimeiroMove { get => PrimeiroMove1; set => PrimeiroMove1 = value; }
    public int PrimeiroMove1 { get => primeiroMove; set => primeiroMove = value; }

    public bool temAI = false;

    //vetor de bool que checa se os squares do Castle estão sob ataque
    public bool[] attackedSquares= new bool[8];
    public bool[] attackedSquaresBlack = new bool[8];

    private bool[] attackedByHorse = new bool[8];

    private bool[] attackedByBishop = new bool[8];

    private bool[] attackedByTower = new bool[8];

    //peça selecionada pela ultima vez
    public PeçaDefault selectedPeça;

    //ultima peça movida pelo branco e pelo preto
    public PeçaDefault lastBlack;
    public PeçaDefault lastWhite;


    public bool isPromote = false;

    public int Px;
    public int Py;

    //variaveis que registram numero de peças capturadas
    public int BispoB = 0;
    public int TorreB = 0;
    public int CavaloB = 0;
    public int PeãoB = 0;
    public bool RainhaB = false;

    public int BispoP = 0;
    public int TorreP = 0;
    public int CavaloP = 0;
    public int PeãoP = 0;
    public bool RainhaP = false;

    int SemSpam = 0;
    //tamanho de uma celula do tabuleiro e o offset q é metade do tamanho
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    //coordenadas da localização do mouse
    public int selectionX = -1;
    public int selectionY = -1;
    
    //lista de peças 
    //lista dos assets
    public List<GameObject> chessmanPrefabs;

    //lista dos assets instanciados
    private List<GameObject> activeChessman;

    //variavel q boolena q fala se a peça selecionada no problema é a correta
    bool Correta = false;
    bool CorretaFInal = false;


    //Variavel que cuida da posição angular das peças no tabuleiro é utilizada no spawn de peça individual
    private Quaternion Orientation = Quaternion.Euler(-90, 180, 0);

    public bool isWhiteTurn = true;

    private int numerodeMoves;

    private int primeiroMove = 0;
    PeçaDefault refmove;
    PeçaDefault refmoveFinal;

    //Função chamada no start do jogo
    private void Start()
    {
        //Instance = this;
        //instanciar todas as peças

        if (!isProblema)
        {
            SpawnAllChessman();
        }

        if (isProblema)
        {
            SpawnProblema();
            numerodeMoves = StanciadorDeProblema.Instance.xStart.Length;
        }

    }

    private void OnEnable()
    {
        Instance = this;
    }

    //função que atualiza a cada frame
    private void Update()
    {
        UpdateSelection ();
        DrawChessBoard ();
        if (!isProblema) {
            CheckAttackedSquares();
        }


        //pega o input do botão esquerdo do mouse pressionado
        if (!temAI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //se o clique for dentro do tabuleiro
                if (selectionX >= 0 && selectionY >= 0)
                {
                    //se nenhuma peça tiver sido selecionada o clique seleciona else move a peça selecionada
                    if (selectedPeça == null)
                    {
                        BoardHighlight.Instance.HidehighLights();
                        //selecionar a peça
                        SelecionePeça(selectionX, selectionY);
                    }
                    else
                    {
                        BoardHighlight.Instance.HidehighLights();
                        Vector3 a = GetTileCenter(selectionX, selectionY);
                        if (selectedPeça.CurrentX != a.x && selectedPeça.CurrentY != a.z)
                        {
                            MovePeça(selectionX, selectionY);
                        }
                        //mova a peça
                        //MovePeça(selectionX, selectionY);
                    }




                }
            }
        }
        else if (temAI && isWhiteTurn)
        {
            if (isProblema)
            {

            }
            if (Input.GetMouseButtonDown(0))
            {
                //se o clique for dentro do tabuleiro
                if (selectionX >= 0 && selectionY >= 0)
                {
                    //se nenhuma peça tiver sido selecionada o clique seleciona else move a peça selecionada
                    if (selectedPeça == null)
                    {
                        BoardHighlight.Instance.HidehighLights();
                        //selecionar a peça
                        SelecionePeça(selectionX, selectionY);
                        if (isProblema)
                        {
                            refmove = Chessmans[selectionX,selectionY];
                            if (refmove == Chessmans[StanciadorDeProblema.Instance.xStart[primeiroMove], StanciadorDeProblema.Instance.yStart[primeiroMove]])
                            {
                                Correta = true;
                            }
                            else
                            {
                                Correta = false;
                            }
                        }

                    }
                    else
                    {
                        BoardHighlight.Instance.HidehighLights();
                        Vector3 a = GetTileCenter(selectionX, selectionY);
                        if (selectedPeça.CurrentX != a.x && selectedPeça.CurrentY != a.z)
                        {
                            
                            MovePeça(selectionX, selectionY);
                            if (isProblema)
                            {
                                refmoveFinal = Chessmans[selectionX, selectionY];
                                if(refmoveFinal == Chessmans[StanciadorDeProblema.Instance.xFinal[primeiroMove], StanciadorDeProblema.Instance.yFinal[primeiroMove]])
                                {
                                    CorretaFInal = true;
                                }
                                else
                                {
                                    CorretaFInal = false;
                                }
                            }
                        }
                        //mova a peça
                        //MovePeça(selectionX, selectionY);
                    }




                }
            }
        }
        else if (temAI && !isWhiteTurn && !isProblema)
        {
            alfa.Clear();
            beta.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Chessmans[i,j] != null)
                    {
                        if (!Chessmans[i, j].isWhite)
                        {
                            PeçaDefault referencia = Chessmans[i, j];
                            AI.Instance.guardaMoves(alfa, referencia);
                        }
                        else if (Chessmans[i, j].isWhite)
                        {
                            PeçaDefault referencia = Chessmans[i, j];
                            AI.Instance.guardaMoves(beta, referencia);
                        }
                    }
                }
            }
            /*int sizeOfList = 0;
            int gerarNumero;
            sizeOfList = alfa.Count;
            gerarNumero = Random.Range(1, sizeOfList);
            
            Debug.Log(gerarNumero);
            Debug.Log(alfa[gerarNumero].x);
            */
            Moves teste = AI.Instance.evaluateMove(alfa, beta);
            SelecionePeça(teste.peçaDoMove.CurrentX, teste.peçaDoMove.CurrentY);
            MovePeça(teste.x, teste.y);
            
            
            

        }
        else if (temAI && !isWhiteTurn && isProblema)
        {
            if (Correta)
            {
                
                if (CorretaFInal)
                {
                    if(primeiroMove + 1 == numerodeMoves)
                    {
                        Debug.Log("fim do game"); 
                    }
                    else
                    {
                        SelecionePeça(StanciadorDeProblema.Instance.xStart[primeiroMove + 1], StanciadorDeProblema.Instance.yStart[primeiroMove + 1]);
                        MovePeça(StanciadorDeProblema.Instance.xFinal[primeiroMove + 1], StanciadorDeProblema.Instance.yFinal[primeiroMove + 1]);
                        primeiroMove += 2;
                    }
                    
                }
                else
                {
                    Debug.Log("sssss");
                    Debug.Log(refmoveFinal);
                    Debug.Log("-----");
                    Debug.Log(refmove);
                    Debug.Log("sssss");
                }
            
            }
            else
            {
                Debug.Log("gameOver");
            }


        }
        if (capturedKing == true)
        {
            capturedKing = false;
            SceneManager.LoadScene("Menu");
        }


        //Debug.Log(Chessmans[0,0].neverMoved);
        //Debug.Log(Chessmans[4, 0].neverMoved);
    }



    public void SelecionePeça(int x, int y)
    {
        //se a posição que foi clicada for nula return
        if(Chessmans[x,y] == null)
        {
            return;
        }

        //caso a a cor da peça não for a mesma cor do turno (se é a vez do branco ou preto)
        else if(Chessmans[x,y].isWhite != isWhiteTurn)
        {
            return;
        }
        else if (isPromote)
        {
            return;
        }
        else
        {
            //guarda a peça clicada na variavel selected peça
            selectedPeça = Chessmans[x, y];

            //guarda na matriz de boll allowedmoves a matriz de booleano q dos movimentos possiveis
            //pra o tipo de peça q está na posição x y da matriz de peças ativas (chessmans)
            allowedMoves = Chessmans[x, y].PossibleMove();

            BoardHighlight.Instance.HighlightAllowedMoves(allowedMoves);
        }
 
    }

    public void MovePeça(int x, int y)
    {
        //checa se a peça pode executar o movimento
        if (allowedMoves[x,y])
        {
            PeçaDefault c = Chessmans[x, y];
            if(c!= null && c.isWhite != isWhiteTurn)
            {
                //capturou a peça

                if(c.GetType() == typeof(Rei))
                {
                    capturedKing = true;
                }

                if (c.GetType() == typeof(Rainha))
                {
                    if (c.isWhite)
                    {
                        RainhaB = true;
                    }
                    else
                    {
                        RainhaP = true;
                    }
                }

                if (c.GetType() == typeof(Bispo))
                {
                    if (c.isWhite)
                    {
                        BispoB += 1;
                    }
                    else
                    {
                        BispoP += 1;
                    }
                }

                if (c.GetType() == typeof(Torre))
                {
                    if (c.isWhite)
                    {
                        TorreB += 1;
                    }
                    else
                    {
                        TorreP += 1;
                    }
                }

                if (c.GetType() == typeof(Cavalo))
                {
                    if (c.isWhite)
                    {
                        CavaloB += 1;
                    }
                    else
                    {
                        CavaloP += 1;
                    }
                }

                if (c.GetType() == typeof(peão))
                {
                    if (c.isWhite)
                    {
                        PeãoB += 1;
                    }
                    else
                    {
                        PeãoP += 1;
                    }
                }

                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);

            }



            ///em suma deleta a peça da matriz e faz o movimento
            Chessmans[selectedPeça.CurrentX, selectedPeça.CurrentY] = null;
            selectedPeça.transform.position = GetTileCenter(x, y);
            selectedPeça.setPosition(x, y);
            Chessmans[x, y] = selectedPeça;

            ///checar se o movimento foi En Passant
            if (y == 5)
            {
                //branco
                if(Chessmans[x, y - 1] != null && isWhiteTurn && Chessmans[x, y].GetType() == typeof(peão) && !Chessmans[x, y - 1].isWhite)
            {
                    if (Chessmans[x, y - 1].GetType() == typeof(peão) && Chessmans[x, y - 1] == lastBlack && Chessmans[x, y - 1].movedTwice)
                    {
                        activeChessman.Remove(Chessmans[x, y - 1].gameObject);
                        Destroy(Chessmans[x, y - 1].gameObject);
                        PeãoP += 1;
                    }

                }
            }
            else if (y == 2)
            {
                //preto
                if (Chessmans[x, y + 1] != null && !isWhiteTurn && Chessmans[x, y].GetType() == typeof(peão) && Chessmans[x, y + 1].isWhite)
                {
                    if (Chessmans[x, y + 1].GetType() == typeof(peão) && Chessmans[x, y + 1] == lastWhite && Chessmans[x, y + 1].movedTwice)
                    {
                        activeChessman.Remove(Chessmans[x, y + 1].gameObject);
                        Destroy(Chessmans[x, y + 1].gameObject);
                        PeãoB += 1;
                    }

                }
            }


            //MOVIMENTO DA PEÇA E A CAPTURA JÁ OCCORREAM A BAIXO DESSA LINHA

            ///MOVIMENTO ESPECIAL DO ROCK CASO O REI FAÇA UM MOV DE 2 ESPAÇOS CONSIGO CAPTAR ISSO E MOVO A TORRE PRA ONDE ELE ESTAVA
            if (Chessmans[x, y].GetType() == typeof(Rei))
            {
                if(Chessmans[x, y].CurrentX == 2 && Chessmans[x,y].neverMoved)
                {
                    PeçaDefault b = Chessmans[0, y];
                    Chessmans[0, y] = null;
                    b.transform.position = GetTileCenter(x + 1, y);
                    b.setPosition(x + 1, y);
                    Chessmans[x + 1, y] = b;


                }
                if (Chessmans[x, y].CurrentX == 6 && Chessmans[x, y].neverMoved)
                {
                    PeçaDefault d = Chessmans[7,y];
                    Chessmans[7, y] = null;
                    d.transform.position = GetTileCenter(x - 1, y);
                    d.setPosition(x - 1, y);
                    Chessmans[x - 1, y] = d;
                    
                }
            }


            


            

            BoardHighlight.Instance.HidehighLights();
            ///PROMOTE
            if (Chessmans[x, y].GetType() == typeof(peão) && y == 7 && isWhiteTurn)
            {
                isPromote = true;
                Chessmans[x, y] = null;
                Destroy(selectedPeça.gameObject);
                Px = x;
                Py = y;
                //isWhiteTurn = !isWhiteTurn;
                Ui.Instance.BotaoRainha.SetActive(true);
                Ui.Instance.BotaoBispo.SetActive(true);
                Ui.Instance.BotaoTorre.SetActive(true);
                isWhiteTurn = !isWhiteTurn;
                return;
            }
            if (Chessmans[x, y].GetType() == typeof(peão) && y == 0 && !isWhiteTurn)
            {
                isPromote = true;
                Chessmans[x, y] = null;
                Destroy(selectedPeça.gameObject);
                Px = x;
                Py = y;
                //isWhiteTurn = !isWhiteTurn;
                Ui.Instance.BotaoRainha.SetActive(true);
                Ui.Instance.BotaoBispo.SetActive(true);
                Ui.Instance.BotaoTorre.SetActive(true);
                isWhiteTurn = !isWhiteTurn;
                return;

            }
            if (Chessmans[x, y].neverMoved)
            {
                Chessmans[x, y].movedTwice = true;
            }
            if (!Chessmans[x, y].neverMoved)
            {
                Chessmans[x, y].movedTwice = false;
            }



            Chessmans[x, y].neverMoved = false;
            if (Chessmans[x, y].isWhite)
            {
                lastWhite = Chessmans[x, y];
            }
            if (!Chessmans[x, y].isWhite)
            {
                lastBlack = Chessmans[x, y];
            }
            BoardHighlight.Instance.HidehighLights();
            isWhiteTurn = !isWhiteTurn;
        }
        selectedPeça = null;
    }


    public void CheckAttackedSquares()
    {
        
        if (isWhiteTurn)
        {
            //for que checa se tem algum cavalo preto de olho em um cavalo branco
            for(int i = 1; i<= 6; i++)
            {
                //se n entrar em nenhum dos ifs j = 0, logo o square n esta ameaçado por cavalos
                int j = 0;
                if (i != 1)
                {
                    if(Chessmans[i - 2, 1] != null && Chessmans[i- 2,1].GetType() == typeof(Cavalo) && Chessmans[i- 2,1].isWhite == false)
                    {
                        attackedByHorse[i] = true;
                        j += 1;
                    }
                }
                if (Chessmans[i - 1, 2] != null && Chessmans[i -1, 2].GetType() == typeof(Cavalo) && Chessmans[i - 1 , 2].isWhite == false)
                {
                    attackedByHorse[i] = true;
                    j += 1;
                }
                if (Chessmans[i + 1, 2] != null && Chessmans[i + 1, 2].GetType() == typeof(Cavalo) && Chessmans[i + 1 , 2].isWhite == false)
                {
                    attackedByHorse[i] = true;
                    j += 1;
                }
                if (i != 6)
                {
                    if (Chessmans[i + 2, 1] != null && Chessmans[i + 2, 1].GetType() == typeof(Cavalo) && Chessmans[i + 2, 1].isWhite == false)
                    {
                        attackedByHorse[i] = true;
                        j += 1;
                    }
                }
                if(j == 0)
                {
                    attackedByHorse[i] = false;
                }
                

            }

            //checar se não existe um aliado protegendo e se existe uma torre ou rainha no caminho

            for(int i = 1; i<= 6; i++)
            {
                bool z = false;
                int j = 1;
                while (z == false)
                {
                    if(Chessmans[i, j] != null)
                    {
                        if (!Chessmans[i, j].isWhite)
                        {
                            if (Chessmans[i, j].GetType() == typeof(Rainha) || Chessmans[i, j].GetType() == typeof(Torre))
                            {
                                attackedByTower[i] = true;
                                z = true;

                            }
                            else
                            {
                                attackedByTower[i] = false;
                                z = true;
                            }
                        }
                        else
                        {
                            attackedByTower[i] = false;
                            z = true;
                        }
                    }
                    else if(j == 7)
                    {
                        z = true;
                    }
                    else
                    {
                        attackedByTower[i] = false;
                        j += 1;
                    }
                }
            }


            //verificando se o square está sob ameaça de bispos ou peões pra esquerda ou direita
            for(int i =1; i<= 6; i++)
            {
                int max = 7 - i;
                bool pieceOnPath = false;
                int j = 1;

                while( pieceOnPath == false)
                {

                    if(Chessmans[i+j,j] != null){
                        if(Chessmans[i + j, j].isWhite == false)
                        {
                            if (Chessmans[i + j, j].GetType() == typeof(Bispo))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else if(Chessmans[i + j, j].GetType() == typeof(Rainha))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else if (Chessmans[i + j, j].GetType() == typeof(peão) && j == 1)
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else
                            {
                                attackedByBishop[i] = false;
                                pieceOnPath = true;
                            }
                        }
                        else
                        {
                            attackedByBishop[i] = false;
                            pieceOnPath = true;
                        }
                    }
                    else if(Chessmans[i + j, j] == null)
                    {
                        attackedByBishop[i] = false;
                    }
                    j += 1;
                    if (j >= max)
                    {
                        pieceOnPath = true;
                    }
                }

                pieceOnPath = false;
                j = 1;
                int maxinverso = 7 - max;
                while (pieceOnPath == false && attackedByBishop[i] == false)
                {
                    if (Chessmans[i - j, j] != null)
                    {
                        if (Chessmans[i - j, j].isWhite == false)
                        {
                            if (Chessmans[i - j, j].GetType() == typeof(Bispo) || Chessmans[i - j, j].GetType() == typeof(Rainha))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else
                            {
                                attackedByBishop[i] = false;
                                pieceOnPath = true;
                            }
                        }
                        else
                        {
                            attackedByBishop[i] = false;
                            pieceOnPath = true;
                        }
                    }
                    else
                    {
                        attackedByBishop[i] = false;
                    }
                    j += 1;
                    if (j >= maxinverso)
                    {
                        pieceOnPath = true;
                    }
                }
            }
            



            //for q faz a média dos 3 vetores que possuem os ataques

            for(int i = 1; i<= 5; i++)
            {
                

                if( attackedByTower[i]|| attackedByHorse[i] || attackedByBishop[i] )
                {
                    attackedSquares[i] = true;
                }
                if (!attackedByTower[i] && !attackedByHorse[i] && !attackedByBishop[i])
                {
                    attackedSquares[i] = false;
                }
            }
        }
        if (!isWhiteTurn)
        {
            //for que checa se tem algum cavalo preto de olho em um cavalo branco
            for (int i = 1; i <= 6; i++)
            {
                //se n entrar em nenhum dos ifs j = 0, logo o square n esta ameaçado por cavalos
                int j = 0;
                if (i != 1)
                {
                    if (Chessmans[i - 2, 6] != null && Chessmans[i - 2, 6].GetType() == typeof(Cavalo) && Chessmans[i - 2, 6].isWhite == true)
                    {
                        attackedByHorse[i] = true;
                        j += 1;
                    }
                }
                if (Chessmans[i - 1, 5] != null && Chessmans[i - 1, 5].GetType() == typeof(Cavalo) && Chessmans[i - 1, 5].isWhite == true)
                {
                    attackedByHorse[i] = true;
                    j += 1;
                }
                if (Chessmans[i + 1, 5] != null && Chessmans[i + 1, 5].GetType() == typeof(Cavalo) && Chessmans[i + 1, 5].isWhite == true)
                {
                    attackedByHorse[i] = true;
                    j += 1;
                }
                if (i != 6)
                {
                    if (Chessmans[i + 2, 6] != null && Chessmans[i + 2, 6].GetType() == typeof(Cavalo) && Chessmans[i + 2, 6].isWhite == true)
                    {
                        attackedByHorse[i] = true;
                        j += 1;
                    }
                }
                if (j == 0)
                {
                    attackedByHorse[i] = false;
                }


            }

            //checar se não existe um aliado protegendo e se existe uma torre ou rainha no caminho
            // for percorre os 5 squares entre o rei e a torre e o square do rei
            for (int i = 1; i <= 6; i++)
            {
                bool z = false;
                int j = 6;
                while (z == false)
                {
                    if (Chessmans[i, j] != null && Chessmans[i, j].isWhite == true)
                    {
                        if (Chessmans[i, j].GetType() == typeof(Rainha) || Chessmans[i, j].GetType() == typeof(Torre))
                        {
                            attackedByTower[i] = true;
                            z = true;

                        }
                        else
                        {
                            attackedByTower[i] = false;
                            z = true;
                        }
                    }
                    else if (Chessmans[i, j] != null && Chessmans[i, j].isWhite == false)
                    {
                        attackedByTower[i] = false;
                        z = true;
                    }
                    else if (j == 0)
                    {
                        z = true;
                    }
                    else
                    {
                        attackedByTower[i] = false;
                        j -= 1;
                    }
                }
            }

            //BUG NO CORNO DO BISHOP
            //verificando se o square está sob ameaça de bispos ou peões pra esquerda ou direita
            for (int i = 1; i <= 6; i++)
            {
                int max = 7 - i;
                bool pieceOnPath = false;

                int j = 1;

                while (pieceOnPath == false)
                {

                    if (Chessmans[i + j, 7 - j] != null)
                    {
                        if (Chessmans[i + j, 7 - j].isWhite == true)
                        {
                            if (Chessmans[i + j, 7 - j].GetType() == typeof(Bispo))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else if (Chessmans[i + j, 7 - j].GetType() == typeof(Rainha))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else if (Chessmans[i + j, 7 - j].GetType() == typeof(peão) && j == 6)
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else
                            {
                                attackedByBishop[i] = false;
                                pieceOnPath = true;
                            }
                        }
                        else
                        {
                            attackedByBishop[i] = false;
                            pieceOnPath = true;
                        }
                    }
                    else if (Chessmans[i + j, 7 - j] == null)
                    {
                        attackedByBishop[i] = false;
                    }
                    j += 1;
                    if (j >= max)
                    {
                        pieceOnPath = true;
                    }
                }
                
                pieceOnPath = false;
                j = 1;
                int maxinverso = 7 - max;
                while (pieceOnPath == false && attackedByBishop[i] == false)
                {
                    if (Chessmans[i - j, 7 - j] != null)
                    {
                        if (Chessmans[i - j, 7 - j].isWhite == true)
                        {
                            //ALGUM BUG POR AQUI
                            if (Chessmans[i - j, 7 - j].GetType() == typeof(Bispo))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else if(Chessmans[i - j, 7 - j].GetType() == typeof(Rainha))
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else if (Chessmans[i - j, 7 - j].GetType() == typeof(peão) && j == 1)
                            {
                                attackedByBishop[i] = true;
                                pieceOnPath = true;
                            }
                            else
                            {
                                attackedByBishop[i] = false;
                                pieceOnPath = true;
                            }
                        }
                        else
                        {
                            attackedByBishop[i] = false;
                            pieceOnPath = true;
                        }
                    }
                    else
                    {
                        attackedByBishop[i] = false;
                    }
                    j += 1;
                    if (j >= maxinverso)
                    {
                        pieceOnPath = true;
                    }
                }
            }





            //for q faz a média dos 3 vetores que possuem os ataques

            for (int i = 1; i <= 5; i++)
            {


                if (attackedByTower[i] || attackedByHorse[i] || attackedByBishop[i])
                {
                    attackedSquaresBlack[i] = true;
                }
                if (!attackedByTower[i] && !attackedByHorse[i] && !attackedByBishop[i])
                {
                    attackedSquaresBlack[i] = false;
                }
            }
        }


    }


    //desenha o tabuleiro e o hover da localização do mouse
    public void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for(int i = 0; i <= 8; i++)
        {
            //aritmética de vetor, start depois da declaração com I=1 fica (x=0,y=0,z=1)
            Vector3 start = Vector3.forward * i;

            //aqui no draw o ponto inicial é start, q é (0,0,1) e o final é start (0,0,1)
            // + widthline q é (8,0,0), logo fica (8,0,1) como ponto final

            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                //mesmo esquema nesse nested for
                Debug.DrawLine(start, start + heightLine);
            }
        }

        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }

    }



    //função que identifica onde o mouse está apontando
    public void UpdateSelection()
    {
        if (!Camera.main)
            return;


        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 25.0f, layerMask: LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }



    //função de instanciamento de peças
    public void SpawnChessman(int index, int x, int y)
    {
        GameObject go = Instantiate (chessmanPrefabs[index], GetTileCenter(x,y), Orientation) as GameObject;
        go.transform.SetParent(transform);
        Chessmans [x, y] = go.GetComponent<PeçaDefault>();
        Chessmans [x, y].setPosition(x, y);
        activeChessman.Add(go);
    }


    //função para spawnar o time aliado
    public void SpawnAllChessman()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new PeçaDefault[8,8];

        // Spawn o time branco
        //rei
        SpawnChessman(0, 3, 0);
        //rainha
        SpawnChessman(1, 4, 0);
        //torre
        SpawnChessman(2,0, 0);
        SpawnChessman(2, 7, 0);
        //bispo
        SpawnChessman(3,2, 0);
        SpawnChessman(3, 5, 0);
        //cavalo
        SpawnChessman(4, 1, 0);
        SpawnChessman(4, 6, 0);
        //pawns
        for (int i = 0; i < 8; i++)
            SpawnChessman(5, i, 1);
  
        // Spawn o time preto
        //rei
        SpawnChessman(6, 4, 7);
        //rainha
        SpawnChessman(7, 3, 7);
        //torre
        SpawnChessman(8, 0, 7);
        SpawnChessman(8, 7, 7);
        //bispo
        SpawnChessman(9, 2, 7);
        SpawnChessman(9, 5, 7);
        //cavalo
        SpawnChessman(10, 1, 7);
        SpawnChessman(10, 6, 7);
        //pawns
        for (int i = 0; i < 8; i++)
            SpawnChessman(11, i, 6);
    }


    public void SpawnProblema()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new PeçaDefault[8, 8];
        int n = StanciadorDeProblema.Instance.xSpawn.Length;
        for(int i =0; i < n; i++)
        {
            SpawnChessman(StanciadorDeProblema.Instance.IndexPeca[i], StanciadorDeProblema.Instance.xSpawn[i], StanciadorDeProblema.Instance.ySpawn[i]);
            if (StanciadorDeProblema.Instance.QuadrantePreto)
            {
                Chessmans[StanciadorDeProblema.Instance.xSpawn[i], StanciadorDeProblema.Instance.ySpawn[i]].isWhite = !Chessmans[StanciadorDeProblema.Instance.xSpawn[i], StanciadorDeProblema.Instance.ySpawn[i]].isWhite;
            }
        }
    }

    //função que encontra o centro de cada quadrado
    public Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y = 0.1f;
        return origin;
 

    }
}
