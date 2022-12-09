namespace DevourDev.Unity.Pools
{
    public sealed class ForcedReleasablePoolableComponent : ForcedReleasableComponent
    {
        public override void Release()
        {
            SendMessage("ReturnToPool");
        }
    }
}
