using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    //�|�[�g��
    //��
    //Linux�ł�/dev/ttyUSB0
    //windows�ł�COM1
    //Mac�ł�/dev/tty.usbmodem1421�Ȃ�
    [SerializeField] string portName = "COM1";
    [SerializeField] int baudRate = 9600;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;
    // �V���A���ʐM�̒l�i�[
    private string message_;
    // �V���A����M�t���b�O
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        Open();
    }

    /*
    read() �ɂ���� isNewMessageReceived_ == true
    delegate�^ OnDataReceived(string message) �̒�`�����\�b�h�ɂ���������
    message�����������
    */
    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false; // false�ɖ߂�
    }

    void OnDestroy()
    {
        Close(); // Close()�Ăяo��
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        //�܂���
        //serialPort_ = new SerialPort(portName, baudRate);
        serialPort_.Open();

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    /*
    OnDestroy() ����Ăяo�����
    */
    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    /*
    �X���b�h�œ������g
    �V���A���f�[�^����M����� message_�ɒl�i�[����
    isNewMessageReceived�� update()�����s�����
    */
    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
