using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script này dùng để tạo một bảng chọn màu cho UI, cho phép người dùng chọn một màu từ danh sách có sẵn.
public class ColorPicker : MonoBehaviour
{
    // Mảng các màu có thể chọn.
    public Color[] AvailableColors;
    // Prefab nút bấm dùng để hiển thị từng màu.
    public Button ColorButtonPrefab;
    
    // Màu hiện tại đang được chọn (chỉ đọc từ bên ngoài).
    public Color SelectedColor { get; private set; }
    // Sự kiện callback khi màu được chọn thay đổi.
    public System.Action<Color> onColorChanged;

    // Danh sách các nút màu đã được tạo ra.
    List<Button> m_ColorButtons = new List<Button>();
    
    // Start is called before the first frame update
    // Hàm khởi tạo bảng chọn màu, tạo nút cho từng màu trong AvailableColors.
    public void Init()
    {
        foreach (var color in AvailableColors)
        {
            // Tạo một nút mới từ prefab và gắn vào UI.
            var newButton = Instantiate(ColorButtonPrefab, transform);
            // Đặt màu cho nút vừa tạo.
            newButton.GetComponent<Image>().color = color;

            // Thêm sự kiện khi nút được bấm.
            newButton.onClick.AddListener(() =>
            {
                // Cập nhật màu đã chọn.
                SelectedColor = color;
                // Đặt tất cả các nút về trạng thái có thể bấm.
                foreach (var button in m_ColorButtons)
                {
                    button.interactable = true;
                }
                // Đặt nút vừa chọn thành không thể bấm (để đánh dấu đang chọn).
                newButton.interactable = false;
                // Gọi callback thông báo màu đã thay đổi.
                onColorChanged.Invoke(SelectedColor);
            });

            // Thêm nút vào danh sách quản lý.
            m_ColorButtons.Add(newButton);
        }
    }

    // Hàm chọn màu theo giá trị Color (dùng cho code, không phải người dùng).
    public void SelectColor(Color color)
    {
        for (int i = 0; i < AvailableColors.Length; ++i)
        {
            // Nếu màu trùng với màu truyền vào thì gọi sự kiện click của nút đó.
            if (AvailableColors[i] == color)
            {
                m_ColorButtons[i].onClick.Invoke();
            }
        }
    }
}
