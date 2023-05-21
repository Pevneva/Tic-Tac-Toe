using System;

namespace Scrips.Cell
{
    public class CellController
    {
        private readonly CellModel _model;
        private readonly CellView _view;
        private int _currentPLayer;

        public event Action CellClicked;

        public CellController(CellModel model, CellView view, int startPlayerNumber)
        {
            _model = model;
            _view = view;
            _currentPLayer = startPlayerNumber;

            _view.Clicked += OnViewClicked;
            _model.StateChanged += OnModelStateChanged;

            RenderImage();
        }

        public void OnPlayerChanged(int currentPlayer) =>
            _currentPLayer = currentPlayer;

        public void RemoveSubscribes()
        {
            _view.Clicked -= OnViewClicked;
            _model.StateChanged -= OnModelStateChanged;
        }

        public void ResetImage() => _view.ResetImage();

        private void OnViewClicked()
        {
            _model.ChangeState(_currentPLayer);
            
            CellClicked?.Invoke();

            _view.Clicked -= OnViewClicked;
        }

        private void OnModelStateChanged(int playerNumber) =>
            RenderImage();

        private void RenderImage()
        {
            switch (_model.State)
            {
                case 0:
                    _view.RenderImage(CellImageType.NONE);
                    break;
                case 1:
                    _view.RenderImage(CellImageType.CROSS);
                    break;
                case 2:
                    _view.RenderImage(CellImageType.ZERO);
                    break;
            }
        }
    }
}