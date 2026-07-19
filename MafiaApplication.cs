using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Mafia
{
    public class MafiaApplication : Game
    {
        public const int TITLE_SCENE = 1;
        public const int SELECT_SCENE_TITLE = 2;
        public const int GAME_SCENE = 3;
        public const int SELECT_SCENE_GAME = 4;

        private GraphicsDeviceManager graphics;

        private MafiaVideo video;
        private MafiaSound sound;
        private MafiaInput input;
        private Song music;

        private TitleScene title;
        private SelectScene select;
        private GameScene game;

        private Stage[] stages;
        private int currentStageIndex;

        private int state;

        public MafiaApplication()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            stages = new Stage[Mafia.NUM_STAGES];
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i] = StageLoader.Load($"stage{i}.stg");
            }
            currentStageIndex = 0;

            title = new TitleScene();
            select = new SelectScene(stages, 0);

            state = TITLE_SCENE;

            video = new MafiaVideo(this);
            sound = new MafiaSound(this);
            input = new MafiaInput();
            music = Song.FromUri("mafia", new Uri("Content/mafia.mp3", UriKind.Relative));
            MediaPlayer.Volume = 0.9f;
            MediaPlayer.IsRepeating = true;

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            if (video != null)
            {
                video.Dispose();
                video = null;
            }

            if (sound != null)
            {
                sound.Dispose();
                sound = null;
            }

            if (input != null)
            {
                input.Dispose();
                input = null;
            }

            MediaPlayer.Stop();

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case TITLE_SCENE:
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                        {
                            MediaPlayer.Stop();
                        }
                        switch (title.Tick(input.GetCurrentTitleInput()))
                        {
                            case TitleScene.NONE:
                                break;

                            case TitleScene.START_GAME:
                                select.CurrentStage = currentStageIndex;
                                state = SELECT_SCENE_TITLE;
                                break;

                            case TitleScene.EXIT:
                                break;
                        }
                    }
                    break;

                case SELECT_SCENE_TITLE:
                    {
                        title.Tick(TitleInput.Empty);
                        int result = select.Tick(input.GetCurrentSelectInput());

                        switch (result)
                        {
                            case SelectScene.NONE:
                                sound.PlaySelectSound(select);
                                break;

                            case SelectScene.GOTO_TITLE:
                                state = TITLE_SCENE;
                                break;

                            default:
                                state = GAME_SCENE;
                                game = stages[currentStageIndex = result].CreateGame();
                                break;
                        }
                    }
                    break;

                case GAME_SCENE:
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(music);
                    }
                    {
                        switch (game.Tick(input.GetCurrentGameInput()))
                        {
                            case GameScene.NONE:
                                sound.PlayGameSound(game);
                                break;

                            case GameScene.RESET_GAME:
                                sound.StopSounds();
                                game = stages[currentStageIndex].CreateGame();
                                break;

                            case GameScene.CLEAR_GAME:
                                sound.StopSounds();
                                currentStageIndex = (currentStageIndex + 1) % stages.Length;
                                game = stages[currentStageIndex].CreateGame();
                                break;

                            case GameScene.SELECT:
                                sound.PlayGameSound(game);
                                select.CurrentStage = currentStageIndex;
                                state = SELECT_SCENE_GAME;
                                break;

                            case GameScene.GOTO_TITLE:
                                sound.StopSounds();
                                state = TITLE_SCENE;
                                break;
                        }
                    }
                    break;

                case SELECT_SCENE_GAME:
                    {
                        game.Tick(GameInput.Empty);
                        int result = select.Tick(input.GetCurrentSelectInput());

                        switch (result)
                        {
                            case SelectScene.NONE:
                                sound.PlayGameSound(game);
                                sound.PlaySelectSound(select);
                                break;

                            case SelectScene.GOTO_TITLE:
                                sound.StopSounds();
                                state = TITLE_SCENE;
                                break;

                            default:
                                sound.StopSounds();
                                game = stages[currentStageIndex = result].CreateGame();
                                state = GAME_SCENE;
                                break;
                        }
                    }
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (state)
            {
                case TITLE_SCENE:
                    video.Begin();
                    video.DrawTitleScene(title);
                    video.End();
                    video.Present();
                    break;

                case SELECT_SCENE_TITLE:
                    video.Begin();
                    video.DrawTitleScene(title);
                    video.DrawSelectScene(select);
                    video.End();
                    video.Present();
                    break;

                case GAME_SCENE:
                    video.Begin();
                    video.DrawGameScene(game);
                    video.End();
                    video.Present();
                    break;

                case SELECT_SCENE_GAME:
                    video.Begin();
                    video.DrawGameScene(game);
                    video.DrawSelectScene(select);
                    video.End();
                    video.Present();
                    break;
            }
        }

        public GraphicsDeviceManager Graphics => graphics;
    }
}
