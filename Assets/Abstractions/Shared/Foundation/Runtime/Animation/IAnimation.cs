namespace Assets.Abstractions.Shared.Foundation.animation
{
	public interface IAnimation
	{
		float Duration { get; }

		void SetTime(float time);
	}
}
