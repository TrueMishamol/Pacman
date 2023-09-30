public class PelletPower : Pellet {


    public float Duration = 8.0f;

    protected override void Eat() {
        FindObjectOfType<PacmanGameManager>().PelletPowerEaten(this);
    }
}
