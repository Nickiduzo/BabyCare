public static class LayoutSliderExtension
{
    public static void TrySlideLeft(this ILayoutSlider slider)
    {
        if(slider.CanSlideLeft)
            slider.SlideLeft();
    }
    
    public static void TrySlideRight(this ILayoutSlider slider)
    {
        if(slider.CanSlidRight)
            slider.SlideRight();
    }
}