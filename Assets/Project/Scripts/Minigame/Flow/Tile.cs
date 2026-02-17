using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Flow
{
    public class Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
    {
        [SerializeField] private Image end;
        [SerializeField] private Image linkUp;
        [SerializeField] private Image linkDown;
        [SerializeField] private Image linkLeft;
        [SerializeField] private Image linkRight;

        public int Index { get; private set; }
        public Color? LinkColor { get; set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDownEvent?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterEvent?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUpEvent?.Invoke();
        }

        public event Action<Tile> PointerDownEvent;
        public event Action<Tile> PointerEnterEvent;
        public event Action PointerUpEvent;

        public void InitializeEnd(int index, Color color)
        {
            Index = index;
            end.color = color;
        }

        public void InitializeNormal(int index)
        {
            Index = index;
        }

        public void Link(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    linkUp.color = LinkColor ?? Color.clear;
                    break;
                case Direction.Down:
                    linkDown.color = LinkColor ?? Color.clear;
                    break;
                case Direction.Left:
                    linkLeft.color = LinkColor ?? Color.clear;
                    break;
                case Direction.Right:
                    linkRight.color = LinkColor ?? Color.clear;
                    break;
            }
        }

        public void Unlink()
        {
            linkUp.color = Color.clear;
            linkDown.color = Color.clear;
            linkLeft.color = Color.clear;
            linkRight.color = Color.clear;

            LinkColor = null;
        }
    }
}