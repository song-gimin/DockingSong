using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DockingSong
{
    public partial class MainForm : Form
    {
        private static DockPanel _dockPanel;
        
        // DockPanel 불러오기
        public MainForm()
        {
            InitializeComponent();

            //_dockPanel = new DockPanel();
            //_dockPanel.Dock = DockStyle.Fill;  // 아래처럼 한번에 하는게 가독성이 더 좋고, _dockPanel 변수 한번만 써도 되고, 더 추가하기도 좋음

            _dockPanel = new DockPanel()
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(_dockPanel);

            _dockPanel.Theme = new VS2015BlueTheme();  // docking 창 테마 설정 (어떤 모양으로 띄울건지)

            LoadDockingWindows();  // docking 창들 불러오기
        }

        // 내부 함수 생성 (private)
        private void LoadDockingWindows()
        {
            // 카메라 창
            var cameraForm = new CameraForm();
            cameraForm.Show(_dockPanel, DockState.Document); //도킹에 쓰려면 붙이는 애들도 도킹화 시켜야함. CameraForm.cs 상속 바꿔주기

            // 검사 결과 창 (카메라 창 아래 30% 비율로 띄우기)
            var resusltForm = new ResultForm();
            resusltForm.Show(cameraForm.Pane, DockAlignment.Bottom, 0.3);

            // 속성 창 (오른쪽에 창 띄우기)
            var propForm = new PropertiesForm();
            propForm.Show(_dockPanel, DockState.DockRight);

            // 속성창에 statistic 창 추가
            var statisticForm = new StatisticForm();
            statisticForm.Show(_dockPanel, DockState.DockRight);

            // 로그 창 (우측 속성 창 만든 영역 아래에 50% 비율로 띄우기)
            var logForm = new LogForm();
            logForm.Show(propForm.Pane, DockAlignment.Bottom, 0.5);  // 위로 띄우고 싶으면 Top 사용하셈~

        }
    }
}
