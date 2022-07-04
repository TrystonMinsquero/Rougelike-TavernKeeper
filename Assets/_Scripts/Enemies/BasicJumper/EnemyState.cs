namespace Enemies
{
    [System.Serializable]
    public abstract class EnemyState : IState
    {
        public string StateName => this.GetType().Name;
        public abstract void Tick();

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}