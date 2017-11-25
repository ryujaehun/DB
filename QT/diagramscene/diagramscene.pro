QT += widgets
QT += sql
HEADERS	    =   mainwindow.h \
		diagramitem.h \
		diagramscene.h \
		arrow.h \
		diagramtextitem.h \
    database.h
SOURCES	    =   mainwindow.cpp \
		diagramitem.cpp \
		main.cpp \
		arrow.cpp \
		diagramtextitem.cpp \
		diagramscene.cpp \
    database.cpp
RESOURCES   =	diagramscene.qrc


# install
#target.path = $$[QT_INSTALL_EXAMPLES]/widgets/graphicsview/diagramscene
#INSTALLS += target
