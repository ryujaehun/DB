


#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "ui_mainwindow.h"

class Document;

class MainWindow : public QMainWindow, public Ui::MainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = 0);

    void addDocument(Document *doc);
    void removeDocument(Document *doc);
    void setCurrentDocument(Document *doc);
    Document *currentDocument() const;

public slots:
    void openDocument();
    void saveDocument();
    void closeDocument();
    void newDocument();

    void addShape();
    void removeShape();
    void setShapeColor();

    void addSnowman();
    void addRobot();

    void about();
    void aboutQt();

private slots:
    void updateActions();

private:
    QUndoGroup *m_undoGroup;

    QString fixedWindowTitle(const Document *doc) const;
};

#endif // MAINWINDOW_H
