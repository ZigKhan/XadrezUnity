using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaCoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(BoardManager.Instance != null)
        {
            BoardManager.Instance.SpawnAllChessman();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        BoardManager.Instance.UpdateSelection();
        BoardManager.Instance.DrawChessBoard();
        //BoardManager.Instance.CheckAttackedSquares();

        if (Input.GetMouseButtonDown(0))
        {
            //se o clique for dentro do tabuleiro
            if (BoardManager.Instance.selectionX >= 0 && BoardManager.Instance.selectionY >= 0)
            {
                //se nenhuma peça tiver sido selecionada o clique seleciona else move a peça selecionada
                if (BoardManager.Instance.selectedPeça == null)
                {
                    BoardHighlight.Instance.HidehighLights();
                    //selecionar a peça
                    BoardManager.Instance.SelecionePeça(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                }
                else
                {
                    BoardHighlight.Instance.HidehighLights();
                    Vector3 a = BoardManager.Instance.GetTileCenter(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                    if (BoardManager.Instance.selectedPeça.CurrentX != a.x && BoardManager.Instance.selectedPeça.CurrentY != a.z)
                    {
                        BoardManager.Instance.MovePeça(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                    }
                    //mova a peça
                    //MovePeça(selectionX, selectionY);
                }




            }
        }
        if (BoardManager.Instance.capturedKing == true)
        {
            BoardManager.Instance.capturedKing = false;
            SceneManager.LoadScene("Menu");
        }
    }
}
