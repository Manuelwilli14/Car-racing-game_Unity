import socket

class UDPSender:
    def __init__(self, ip="127.0.0.1", port=5052):
        self.ip = ip
        self.port = port
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    def send(self, gesture):
        message = gesture.encode("utf-8")
        self.sock.sendto(message, (self.ip, self.port))
        print(f"Envoyé : {gesture}")