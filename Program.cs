using System;
using System.Collections.Generic;

class Jogador
{
    public string Nome { get; set; }  // Propriedade para o nome do jogador.
    public string Nickname { get; set; }  // Propriedade para o apelido do jogador.
    public int Pontos { get; private set; }  // Propriedade para os pontos do jogador, com setter privado para evitar alterações externas.

    public Jogador(string nome, string nickname)  // Construtor da classe Jogador.
    {
        Nome = nome;
        Nickname = nickname;
        Pontos = 0;  // Inicializa os pontos do jogador como zero.
    }

    public void Jogar()  // Método que simula uma partida para o jogador.
    {
        Random random = new Random();  // Cria uma instância da classe Random para gerar números aleatórios.
        int pontosDaPartida = random.Next(1, 101);  // Gera um número aleatório entre 1 e 100 para representar os pontos da partida.
        Pontos += pontosDaPartida;  // Adiciona os pontos da partida aos pontos totais do jogador.
        Console.WriteLine($"{Nickname} obteve {pontosDaPartida} pontos na partida.");  // Exibe uma mensagem com o resultado da partida.
    }
}

class Equipe
{
    public string NomeEquipe { get; set; }  // Propriedade para o nome da equipe.
    public List<Jogador> Jogadores { get; private set; }  // Lista de jogadores que compõem a equipe.

    public Equipe(string nomeEquipe)  // Construtor da classe Equipe.
    {
        NomeEquipe = nomeEquipe;
        Jogadores = new List<Jogador>();  // Inicializa a lista de jogadores vazia.
    }

    public int PontosTotal()  // Método que calcula a pontuação total da equipe somando os pontos de todos os jogadores.
    {
        int pontosTotal = 0;
        foreach (Jogador jogador in Jogadores)  // Percorre a lista de jogadores da equipe.
        {
            pontosTotal += jogador.Pontos;  // Adiciona os pontos de cada jogador ao total.
        }
        return pontosTotal;  // Retorna a pontuação total da equipe.
    }

    public void AdicionarJogador(Jogador jogador)  // Método para adicionar um jogador à equipe.
    {
        if (Jogadores.Count < 5)  // Verifica se a equipe já possui 5 jogadores.
        {
            Jogadores.Add(jogador);  // Adiciona o jogador à equipe.
            Console.WriteLine($"{jogador.Nickname} foi adicionado à equipe {NomeEquipe}.");  // Exibe uma mensagem de confirmação.
        }
        else
        {
            Console.WriteLine($"A equipe {NomeEquipe} já possui 5 jogadores, não é possível adicionar mais jogadores.");  // Exibe uma mensagem de erro.
        }
    }
}

class Campeonato
{
    public string NomeCampeonato { get; set; }  // Propriedade para o nome do campeonato.
    public List<Equipe> EquipesParticipantes { get; private set; }  // Lista de equipes que participam do campeonato.

    public Campeonato(string nomeCampeonato)  // Construtor da classe Campeonato.
    {
        NomeCampeonato = nomeCampeonato;
        EquipesParticipantes = new List<Equipe>();  // Inicializa a lista de equipes vazia.
    }

    public void IniciarPartida(Equipe e1, Equipe e2)  // Método para iniciar uma partida entre duas equipes.
    {
        if (e1.Jogadores.Count == 5 && e2.Jogadores.Count == 5)  // Verifica se ambas as equipes têm exatamente 5 jogadores.
        {
            Console.WriteLine($"Iniciando partida entre {e1.NomeEquipe} e {e2.NomeEquipe}...");  // Exibe uma mensagem indicando o início da partida.
            foreach (Jogador jogador in e1.Jogadores)
            {
                jogador.Jogar();  // Chama o método Jogar() para cada jogador da equipe 1.
            }
            foreach (Jogador jogador in e2.Jogadores)
            {
                jogador.Jogar();  // Chama o método Jogar() para cada jogador da equipe 2.
            }
            Console.WriteLine($"A partida entre {e1.NomeEquipe} e {e2.NomeEquipe} terminou.");  // Exibe uma mensagem indicando o término da partida.
        }
        else
        {
            Console.WriteLine("Ambas as equipes devem ter exatamente 5 jogadores para iniciar a partida.");  // Exibe uma mensagem de erro.
        }
    }

    public void Classificacao()  // Método para exibir a classificação das equipes no campeonato.
    {
        EquipesParticipantes.Sort((e1, e2) => e2.PontosTotal().CompareTo(e1.PontosTotal()));  // Ordena as equipes com base na pontuação total, da maior para a menor.
        Console.WriteLine("Classificação do Campeonato:");
        int posicao = 1;
        foreach (Equipe equipe in EquipesParticipantes)
        {
            Console.WriteLine($"{posicao}. {equipe.NomeEquipe} - {equipe.PontosTotal()} pontos");  // Exibe a classificação de cada equipe.
            posicao++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Campeonato campeonato = new Campeonato("Meu Campeonato");  // Cria uma instância de Campeonato.

        while (true)  // Loop infinito para o menu interativo.
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Criar jogador");
            Console.WriteLine("2. Criar equipe");
            Console.WriteLine("3. Adicionar jogador à equipe");
            Console.WriteLine("4. Iniciar partida");
            Console.WriteLine("5. Ver classificação");
            Console.WriteLine("6. Sair");

            int opcao = int.Parse(Console.ReadLine());  // Lê a opção escolhida pelo usuário.

            switch (opcao)
            {
                case 1:
                    Console.Write("Nome do jogador: ");
                    string nomeJogador = Console.ReadLine();
                    Console.Write("Nickname do jogador: ");
                    string nicknameJogador = Console.ReadLine();
                    Jogador jogador = new Jogador(nomeJogador, nicknameJogador);  // Cria um jogador com nome e apelido informados.
                    break;

                case 2:
                    Console.Write("Nome da equipe: ");
                    string nomeEquipe = Console.ReadLine();
                    Equipe equipe = new Equipe(nomeEquipe);  // Cria uma equipe com nome informado.
                    campeonato.EquipesParticipantes.Add(equipe);  // Adiciona a equipe ao campeonato.
                    break;

                case 3:
                    Console.Write("Nickname do jogador: ");
                    string nickname = Console.ReadLine();
                    Console.Write("Nome da equipe: ");
                    string nomeEquipeParaAdicionar = Console.ReadLine();
                    Equipe equipeParaAdicionar = campeonato.EquipesParticipantes.Find(e => e.NomeEquipe == nomeEquipeParaAdicionar);  // Encontra a equipe pelo nome.
                    if (equipeParaAdicionar != null)
                    {
                        Jogador jogadorParaAdicionar = new Jogador("NomeDoJogador", nickname);  // Cria um jogador com apelido informado.
                        equipeParaAdicionar.AdicionarJogador(jogadorParaAdicionar);  // Adiciona o jogador à equipe.
                    }
                    else
                    {
                        Console.WriteLine("Equipe não encontrada.");
                    }
                    break;

                case 4:
                    Console.Write("Nome da primeira equipe: ");
                    string nomeEquipe1 = Console.ReadLine();
                    Console.Write("Nome da segunda equipe: ");
                    string nomeEquipe2 = Console.ReadLine();
                    Equipe equipe1 = campeonato.EquipesParticipantes.Find(e => e.NomeEquipe == nomeEquipe1);  // Encontra a primeira equipe pelo nome.
                    Equipe equipe2 = campeonato.EquipesParticipantes.Find(e => e.NomeEquipe == nomeEquipe2);  // Encontra a segunda equipe pelo nome.
                    campeonato.IniciarPartida(equipe1, equipe2);  // Inicia a partida entre as duas equipes.
                    break;

                case 5:
                    campeonato.Classificacao();  // Exibe a classificação das equipes no campeonato.
                    break;

                case 6:
                    Environment.Exit(0);  // Encerra o programa.
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");  // Exibe uma mensagem de erro para opções inválidas.
                    break;
            }
        }
    }
}
