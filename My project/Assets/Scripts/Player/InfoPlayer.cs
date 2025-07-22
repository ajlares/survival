using UnityEngine;

public class InfoPlayer : MonoBehaviour
{
    private string usuario;
    private string[] adj = {"Pixelated", "Salty", "Epic", "Chonky", "Sweaty", "Cracked", "Laggy", "Turbo","Yeet", "Shadow", "Dank", "Nerfed", "Buffed", "Ragey", "Greedy", "NoScope", "Saucy", "Looted", "404", "Speedy", "Cursed", "Viral"};
    private string[] noun = {"Wizard", "Banana", "Goblin", "Sniper", "Dragon", "Hamster", "Glitch", "Doge", "Nugget", "Mage", "Bronnie", "Waffle", "Zombie", "Lootbox", "Knight", "Capybara", "Orc", "NPC", "Speedrunner", "Duck", "Looter", "Boss", "Skeleton", "Gremlin", "Crab"};
    //[SerializeField] private 
    //[SerializeField] private Material skinMaterial;
    //[SerializeField] private Material hairMaterial;
    //[SerializeField] private Material underwearMaterial;
    //[SerializeField] private Material eyesMaterial;
    //[SerializeField] private Material shoesMaterial;
    public string PideUsuario()
    {
        return GeneraAleatorio();
    }
    
    private string GeneraAleatorio()
    {
        string a = adj[Random.Range(0, adj.Length)];
        string n = noun[Random.Range(0, noun.Length)];
        string c = Random.Range(10, 100).ToString();
        return $"{a}{n}{c}";
    }
}
