public interface ILayoutSlider
{
    bool CanSlideLeft { get; }
    bool CanSlidRight { get; }

    void SlideLeft();
    void SlideRight();
}