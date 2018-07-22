﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Input;
using osu.Game.Graphics.Sprites;
using System;

namespace osu.Game.Graphics.UserInterface
{/// <summary>
/// 
/// </summary>
    public class ClickableText : OsuSpriteText, IHasTooltip
    {
        private bool isEnabled;
        private bool isMuted;
        private SampleChannel sampleHover;
        private SampleChannel sampleClick;

        /// <summary>
        /// An action that can be set to execute after click.
        /// </summary>
        public Action Action;

        /// <summary>
        /// If set to true, a sound will be played on click.
        /// </summary>
        public bool IsClickMuted;

        /// <summary>
        /// If set to true, a sound will be played on hover.
        /// </summary>
        public bool IsHoverMuted;

        /// <summary>
        /// If disabled, no sounds will be played and <see cref="Action"/> wont execute.
        /// True by default.
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                this.FadeTo(value ? 1 : 0.5f, 250);
            }
        }

        /// <summary>
        /// Whether to play sounds on hover and click. Automatically sets
        /// <see cref="IsClickMuted"/> and <see cref="IsHoverMuted"/> to the same value.>
        /// </summary>
        public bool IsMuted {
            get { return isMuted; }
            set
            {
                IsHoverMuted = value;
                IsClickMuted = value;
                isMuted = value;
            }
        }

        /// <summary>
        /// A text with sounds on hover and click,
        /// an action that can be set to execute on click,
        /// and a tooltip.
        /// </summary>
        public ClickableText() => isEnabled = true;

        public override bool HandleMouseInput => true;

        protected override bool OnHover(InputState state)
        {
            if (isEnabled && !IsHoverMuted)
                sampleHover?.Play();
            return base.OnHover(state);
        }

        protected override bool OnClick(InputState state)
        {
            if (isEnabled)
            {
                if (!IsClickMuted)
                    sampleClick?.Play();
                Action?.Invoke();
            }
            return base.OnClick(state);
        }

        public string TooltipText { get; set; }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            sampleClick = audio.Sample.Get(@"UI/generic-select-soft");
            sampleHover = audio.Sample.Get(@"UI/generic-hover-soft");
        }
    }
}
