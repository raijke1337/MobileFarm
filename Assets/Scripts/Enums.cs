
public class Enums
{
    public enum GameState
    {
        None,
        Menu,
        Gameplay
    }
    public enum UnitType
    {
        None,
        Player,
        Animal,
        Pest
    }
    public enum ItemType : byte
    {
        None,
        Hoe,
        Pail,
        Shovel,
        Sickle,
        Seed,
        Fruit
    }

    public enum SoilState : byte
    {
        None = 0,
        Rocks = 1,
        Water = 2,
        Grass = 3,
        Cut = 4,
        Hoed = 5,
        Watered = 6
    }

}
