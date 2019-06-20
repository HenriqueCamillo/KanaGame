using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo comportamento do menu lateral
/// </summary>
public class SideMenu : MonoBehaviour
{
    // Distância mínima que o jogador deve mover o dedo para abrir o menu
    [SerializeField] private float swipeMinDistance;

    // Velocidade com qual o menu se movimenta,
    // e tolerância de distância para considerar que o menu chegou ao sua posição de destino
    [SerializeField] private float speed, tolerance;

    // Posições dele fechado e aberto
    [SerializeField] private GameObject closedPosition, openPosition;

    // Nova posição do menu, caso ele vá se mover
    private Vector3 newPosition;

    // Início e final do toque na tela, para verificar swipe
    private Vector2 touchBegin, touchEnd;

    // Variável que define se o menu irá se mover
    [SerializeField] private bool moving = false;

    /// <summary>
    /// Faz o menu começar fechado
    /// </summary>
    void Start()
    {
        this.transform.position = closedPosition.transform.position;
    }

    /// <summary>
    /// Detecta o input do jogador
    /// </summary>
    private void Update()
    {   
        // Se houve um toque na tela, salva sua posição inicial
        if (Input.GetMouseButtonDown(0))
            touchBegin = Input.mousePosition;

        // Ao final do toque, sua posição é guardada e verifica-se se houve um swipe
        if (Input.GetMouseButtonUp(0))
        {
            touchEnd = Input.mousePosition;
            DetectSwipe();
        }
    }

    /// <summary>
    /// Detecta se houve um o comando de swipe para abrir ou fechar o menu lateral
    /// </summary>
    private void DetectSwipe() 
    {
        // Pega o vetor do movimento e verifica se o deslizamento no eixo x foi o suficiente para ser considerado
        Vector2 swipe = touchEnd - touchBegin;
        if (Mathf.Abs(swipe.x) > swipeMinDistance)
        {
            // Dependendo da direção do deslizamento, abre ou fecha o menu
            if (swipe.x > 0f)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    /// <summary>
    /// Move o menu lateral
    /// </summary>
    private void FixedUpdate()
    {
        // Caso seja ativada a variável de mover
        if (moving)
        {
            // Move de modo suave o menu lateral para sua nova posição
            this.transform.position = Vector3.Lerp(this.transform.position, newPosition, speed);

            // Se já chegou bem perto do destino, desativa a variável responsável pelo movimento
            if (Mathf.Abs((this.transform.position - newPosition).magnitude) < tolerance)
                moving = false;
        }
    }

    /// <summary>
    /// Fecha o menu
    /// </summary>
    private void CloseMenu()
    {
        newPosition = closedPosition.transform.position;
        moving = true;
    }

    /// <summary>
    /// Abre o menu
    /// </summary>
    private void OpenMenu()
    {
        newPosition = openPosition.transform.position;
        moving = true;
    }
}