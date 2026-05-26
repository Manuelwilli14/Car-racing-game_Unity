class GestureClassifier:
    def classify(self, landmarks):
        if not landmarks:
            return "NONE"

        # On récupère les points clés
        wrist = landmarks[0]      # poignet
        index = landmarks[5]      # base index
        pinky = landmarks[17]     # base auriculaire

        # Inclinaison gauche/droite basée sur la position du poignet
        tilt = wrist[0] - 0.5    # 0.5 = centre de l'écran

        # Main ouverte = tous les doigts étendus (accélérer)
        fingers_up = self._count_fingers_up(landmarks)

        if fingers_up >= 4:
            return "FORWARD"
        elif tilt < -0.1:
            return "LEFT"
        elif tilt > 0.1:
            return "RIGHT"
        else:
            return "NONE"

    def _count_fingers_up(self, landmarks):
        # Tips = bouts des doigts (index 8,12,16,20)
        # PIP = jointure du milieu (index 6,10,14,18)
        tips = [8, 12, 16, 20]
        pips = [6, 10, 14, 18]
        count = 0
        for tip, pip in zip(tips, pips):
            if landmarks[tip][1] < landmarks[pip][1]:  # y plus haut = doigt levé
                count += 1
        return count