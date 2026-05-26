import cv2
from hand_detector import HandDetector
from gesture_classifier import GestureClassifier
from sender import UDPSender

detector = HandDetector()
classifier = GestureClassifier()
sender = UDPSender()

cap = cv2.VideoCapture(0)

while True:
    ret, frame = cap.read()
    if not ret:
        break

    frame, landmarks = detector.detect(frame)
    gesture = classifier.classify(landmarks)
    sender.send(gesture)

    # Affichage du geste détecté
    color = (0, 255, 0) if gesture != "NONE" else (0, 0, 255)
    cv2.putText(frame, gesture, (10, 50),
                cv2.FONT_HERSHEY_SIMPLEX, 1.5, color, 3)

    cv2.imshow("Gesture Controller", frame)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()