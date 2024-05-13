using UnityEngine;

namespace EventsUI
{
    public class UIEventsManager : MonoBehaviour
    {
        private static UIEventsManager _instance;
        private static readonly object _lock = new object();

        public static UIEventsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = FindFirstObjectByType<UIEventsManager>();
                            if (_instance == null)
                            {
                                GameObject obj = new GameObject("UIEventsManager");
                                _instance = obj.AddComponent<UIEventsManager>();
                                DontDestroyOnLoad(obj);
                            }
                        }
                    }
                }
                return _instance;
            }
        }

        public struct MessageData
        {
            public string Message;
            public object OptionalParam;

            public MessageData(string message, object optionalParam = null)
            {
                Message = message;
                OptionalParam = optionalParam;
            }
        }

        public delegate void SendMessageDelegate(MessageData data);
        public event SendMessageDelegate OnSendMessage;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Eventos do Cronômetro
        public void StartStopwatch() => OnSendMessage?.Invoke(new MessageData("Iniciar Cronometro"));
        public void StopStopwatch() => OnSendMessage?.Invoke(new MessageData("Parar Cronometro"));
        public void RestartStopwatch() => OnSendMessage?.Invoke(new MessageData("Reiniciar Cronometro"));
        public void ShowCharacter() => OnSendMessage?.Invoke(new MessageData("Mostrar Personagem"));
        public void HideCharacter() => OnSendMessage?.Invoke(new MessageData("Ocultar Personagem"));

    }
}



