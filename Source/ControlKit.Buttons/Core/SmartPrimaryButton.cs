﻿namespace Nulo.Modules.ControlKit.Buttons {

    public class SmartPrimaryButton : Button {

        #region Properties

        #region BackColor

        private Color backColor = SystemColors.Highlight;

        [Category("Smart")]
        public new Color BackColor {
            get { return backColor; }
            set {
                base.BackColor = backColor = value;
                Invalidate();
            }
        }

        #endregion BackColor

        #region ForeColor

        private Color foreColor = Color.White;

        [Category("Smart")]
        public new Color ForeColor {
            get { return foreColor; }
            set {
                base.ForeColor = foreColor = value;
                Invalidate();
            }
        }

        #endregion ForeColor

        #region DisabledBackColor

        private Color disabledBackColor = SystemColors.HotTrack;

        [Category("Smart")]
        public Color DisabledBackColor {
            get { return disabledBackColor; }
            set {
                disabledBackColor = value;
                Invalidate();
            }
        }

        #endregion DisabledBackColor

        #region DisabledForeColor

        private Color disabledForeColor = Color.WhiteSmoke;

        [Category("Smart")]
        public Color DisabledForeColor {
            get { return disabledForeColor; }
            set {
                disabledForeColor = value;
                Invalidate();
            }
        }

        #endregion DisabledForeColor

        // ...

        #region BorderRadius

        private int borderRadius = 20;

        [Category("Smart")]
        public int BorderRadius {
            get { return borderRadius; }
            set {
                borderRadius = value;
                Invalidate();
            }
        }

        #endregion BorderRadius

        // ...

        #region FlatStyle

        [Browsable(false)]
        public new FlatStyle FlatStyle {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        #endregion FlatStyle

        #region FlatAppearance

        [Browsable(false)]
        public new FlatButtonAppearance FlatAppearance {
            get { return base.FlatAppearance; }
        }

        #endregion FlatAppearance

        #endregion Properties

        #region Constructors

        public SmartPrimaryButton() {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;

            base.BackColor = BackColor;
            base.ForeColor = ForeColor;

            EnabledChanged += new EventHandler(Button_EnabledChanged);
        }

        #endregion Constructors

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);

            using var penSurface = new Pen(Parent.BackColor, 2);
            using var pathSurface = SmartButton.GetFigurePath(ClientRectangle, BorderRadius);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Region = new Region(pathSurface);
            pevent.Graphics.DrawPath(penSurface, pathSurface);

            if(!Enabled) {
                pevent.Graphics.FillRectangle(new SolidBrush(DisabledBackColor), new RectangleF(BorderRadius / 2, BorderRadius / 2, Width - BorderRadius, Height - BorderRadius));
                pevent.Graphics.DrawString(Text, Font, new SolidBrush(DisabledForeColor), new RectangleF(-1, 0, Width, Height), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        #endregion Protected Methods

        #region Private Methods

        private void Button_EnabledChanged(object sender, EventArgs e) {
            base.BackColor = Enabled ? BackColor : DisabledBackColor;
        }

        private void Container_BackColorChanged(object sender, EventArgs e) {
            Invalidate();
        }

        #endregion Private Methods
    }
}