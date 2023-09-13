namespace Sound
{
    public interface ISoundSystem
    {
        void PlaySound(string name);
        void StopSound(string name);
    }
}