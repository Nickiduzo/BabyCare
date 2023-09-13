using System;
using UnityEngine;

public class LayoutSlider : ILayoutSlider
{
    private readonly ILayoutElement[] _elements;
    private readonly float _slideMotion;
    private readonly Vector3 _axis;
    private int _currentElementId;

    public LayoutSlider(ILayoutElement[] elements, float slideMotion, Vector3 axis, int currentElementId = 0)
    {
        if (slideMotion < 0)
            throw new ArgumentOutOfRangeException(nameof(slideMotion));
        if (currentElementId < 0 || currentElementId > elements.Length)
            throw new ArgumentOutOfRangeException(nameof(currentElementId));
        foreach (var layoutElement in elements)
        {
            if (layoutElement == null)
                throw new ArgumentNullException(nameof(layoutElement));
        }

        _elements = elements;
        _slideMotion = slideMotion;
        _axis = axis;
        _currentElementId = currentElementId;
        _elements[_currentElementId].Activate();
    }

    public bool CanSlideLeft => _currentElementId + 1 < _elements.Length;
    public bool CanSlidRight => _currentElementId - 1 >= 0;

    public void SlideLeft()
    {
        if (!CanSlideLeft)
            throw new InvalidOperationException(nameof(SlideLeft));

        Slide(1);
    }

    public void SlideRight()
    {
        if (!CanSlidRight)
            throw new InvalidOperationException(nameof(SlideRight));

        Slide(-1);
    }

    private void Slide(int direction)
    {
        _elements[_currentElementId].Deactivate();

        _currentElementId += direction;

        foreach (var element in _elements)
            element.Translate(_axis * _slideMotion * -direction);

        _elements[_currentElementId].Activate();
    }
}