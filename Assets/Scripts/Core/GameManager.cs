using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Modin
{
    public class GameManager : MonoSingleton<GameManager>
    {
        /*
         * 역할
         *  - 인트로 및 타이틀 진입
         *  - 게임 저장
         *  - 게임 상태 관리
         */
        
        /*
         * 게임 상태
         *  - 인트로: 타이틀 전 진입 화면
         *  - 타이틀: 게임의 시작 화면
         *  - 게임 플레이: 실제 플레이 화면
         *  - 로딩: 게임 흐름 전환 시 대기 화면
         */

        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private Canvas mainCanvas;

        private GameState currentState;
        private List<SaveData> saveData;
        
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(mainCanvas.gameObject);
        }

        private void Start()
        {
            LoadGame();
            Enter(GameState.Intro);
        }

        public void SaveGame()
        {
            
        }

        public void LoadGame()
        {
            
        }

        public void Enter(GameState gameState)
        {
            if (currentState == gameState)
                return;
            
            switch (currentState)
            {
                case GameState.Intro:
                    ExitIntro();
                    break;
                case GameState.Title:
                    ExitTitle();
                    break;
                case GameState.Gameplay:
                    ExitGameplay();
                    break;
            }

            currentState = gameState;
            
            switch (gameState)
            {
                case GameState.Intro:
                    StartCoroutine(EnterIntro());
                    break;
                case GameState.Title:
                    StartCoroutine(EnterTitle());
                    break;
                case GameState.Gameplay:
                    StartCoroutine(EnterGameplay());
                    break;
            }
        }
        
        private IEnumerator EnterIntro()
        {
            /*
             * 순서
             *  1. 로딩 UI
             *  2. 인트로 UI
             *  3. 타이틀로 진입
             */
            
            yield break;
        }
        
        private IEnumerator EnterTitle()
        {
            /*
             * 순서
             *  1. 타이틀 UI
             */
            
            yield break;
        }
        
        private IEnumerator EnterGameplay()
        {
            /*
             * 순서
             *  1. 로딩 UI 열기
             *  2. 대화 UI
             */
            
            yield break;
        }

        private void ExitIntro()
        {
            /*
             * 순서
             *  1. 인트로 UI 닫기
             */
        }
        
        private void ExitTitle()
        {
            /*
             * 순서
             *  1. 타이틀 UI 닫기
             */
        }
        
        private void ExitGameplay()
        {
            /*
             * 순서
             *  1. 대화 UI 닫기
             */
        }
        
        private void Loading()
        {
            
        }
    }
}