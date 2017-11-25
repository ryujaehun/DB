

#ifndef DOCUMENT_H
#define DOCUMENT_H

#include <QWidget>

QT_FORWARD_DECLARE_CLASS(QUndoStack)
QT_FORWARD_DECLARE_CLASS(QTextStream)

class Shape
{
public:
    enum Type { Rectangle, Circle, Triangle };              //도형이름 설정하는 ENUM

    explicit Shape(Type type = Rectangle, const QColor &color = Qt::red, const QRect &rect = QRect());

    Type type() const;                                      //도형 타입 반환
    QString name() const;                                   //이름 반환
    QRect rect() const;                                     //도형 좌표사용할 때 사용하는듯? 도형의 외접 사각형의 좌상단 좌표 + 우하단 좌표 사용? Qrect반환
    QRect resizeHandle() const;                             //크기 변환 핸들반환
    QColor color() const;                                   //색깔 반환

    static QString typeToString(Type type);
    static Type stringToType(const QString &s, bool *ok = 0);

    static const QSize minSize;

private:
    Type m_type;
    QRect m_rect;
    QColor m_color;
    QString m_name;

    friend class Document;
};

class Document : public QWidget
{
    Q_OBJECT

public:
    Document(QWidget *parent = 0);

    QString addShape(const Shape &shape);
    void deleteShape(const QString &shapeName);
    Shape shape(const QString &shapeName) const;
    QString currentShapeName() const;

    void setShapeRect(const QString &shapeName, const QRect &rect);
    void setShapeColor(const QString &shapeName, const QColor &color);

    bool load(QTextStream &stream);
    void save(QTextStream &stream);

    QString fileName() const;
    void setFileName(const QString &fileName);

    QUndoStack *undoStack() const;

signals:
    void currentShapeChanged(const QString &shapeName);

protected:
    void paintEvent(QPaintEvent *event) override;
    void mousePressEvent(QMouseEvent *event) override;                      //이벤트 핸들 하기 위한 오버라이드 함수들임
    void mouseReleaseEvent(QMouseEvent *event) override;                    //(ex-마우스 클릭시 mousePressEvnet발동)
    void mouseMoveEvent(QMouseEvent *event) override;

private:
    void setCurrentShape(int index);
    int indexOf(const QString &shapeName) const;
    int indexAt(const QPoint &pos) const;
    QString uniqueName(const QString &name) const;

    QList<Shape> m_shapeList;                                               //생성한 도형들 목록
    int m_currentIndex;
    int m_mousePressIndex;                                                  //마우스 누른 도형의 인덱스
    QPoint m_mousePressOffset;
    bool m_resizeHandlePressed;
    QString m_fileName;

    QUndoStack *m_undoStack;
};

#endif // DOCUMENT_H
