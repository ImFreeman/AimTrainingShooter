using Zenject;
using UnityEngine;

namespace Features.Player
{
    public class CreatePlayerCommand
    {
        public CreatePlayerCommand(IInstantiator instantiator, PlayerConfig config)
        {
            var playerView = instantiator.InstantiatePrefabResourceForComponent<PlayerView>("Player/Prefab/Player");            
            var playerCamRot = instantiator.Instantiate<PlayerCameraRotation>(
                new object[]
                {
                new PlayerCameraRotationProtocol(
                    playerView.MainCamera.transform,
                    playerView.BodyTransform,
                    config.Sensivity)
                });
            playerCamRot.SetActive(true);

            var weaponController = instantiator.Instantiate<WeaponController>(
                new object[]
                {
                new WeaponControllerProtocol(playerView.MainCamera.transform)
                });
            var visualEffects = instantiator.Instantiate<PlayerVisualEffectsController>(new object[] { new PlayerAnimationProtocol(playerView) });
            var soundEffects = instantiator.Instantiate<PlayerSoundController>(new object[] { new PlayerSoundControllerProtocol(playerView, config.FireSound) });
        }
    }
}