using DockingSong.Property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DockingSong
{
    // 속성창에 사용할 타입 선언(여러개가 생길 수 있으니 enum 으로 구분)
        public enum PropertyType
    {
        Binary,
        Filter
    }
    public partial class PropertiesForm : DockContent
    {
        // 속성 탭을 관리하기 위한 딕셔너리
        Dictionary<string, TabPage> _allTabs = new Dictionary<string, TabPage>();

        public PropertiesForm()
        {
            InitializeComponent();

            // 속성 탭 초기화
            LoadOptionControl(PropertyType.Filter);
            LoadOptionControl(PropertyType.Binary);
        }

        // 속성 탭 생성 : 부모변수로 받음
        private UserControl CreateUserControl (PropertyType propType)
        {
            UserControl curProp = null;

            switch (propType)
            {
                case PropertyType.Binary:
                    BinaryProp blobProp = new BinaryProp();
                    curProp = blobProp;
                    break;
                case PropertyType.Filter:
                    ImageFilterProp filterProp = new ImageFilterProp();
                    curProp = filterProp;
                    break;
                default:
                    MessageBox.Show("유효하지 않은 옵션입니다.");
                    return null;
            }
            return curProp;
        }

        // 속성 탭이 있다면 반환하고, 없다면 새로 생성하기
        private void LoadOptionControl(PropertyType propType)
        {
            string tabName = propType.ToString();

            // 이미 탭이 존재하는지 확인
            foreach (TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Name == tabName) return;
            }

            // 딕셔너리에 있으면 추가
            if (_allTabs.TryGetValue(tabName, out TabPage page))
            {
                tabPropControl.TabPages.Add(page);
                return;
            }

            // 새로운 UserControl 생성
            UserControl _inspProp = CreateUserControl(propType);
            if (_inspProp == null) return; // UserControl 생성이 실패했을 때 방어

            // 새로운 tab 생성
            TabPage newTab = new TabPage(tabName)
            {
                Dock = DockStyle.Fill
            };

            _inspProp.Dock = DockStyle.Fill;
            newTab.Controls.Add(_inspProp);

            tabPropControl.TabPages.Add(newTab);
            tabPropControl.SelectedTab = newTab; // 새 탭 선택

            _allTabs[tabName] = newTab;
        }
    }
}
