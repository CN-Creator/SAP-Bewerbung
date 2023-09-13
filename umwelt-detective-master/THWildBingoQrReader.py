import sys

# adding SDK path to project file --> add here your python sdk library
# sys.path.append("C:\Python27\Lib\site-packages")
# sys.path.append('/Users/benjaminstahl/Documents/Pepper_Python_SDK/lib')
# path for python on MAC from Choregraphe:
# /Applications/Aldebaran/Choregraphe_Suite_2.5.5/Choregraphe_2.5.5.app/Contents/Resources/bin/python2
# MORE INFOS FOR @qi.bind: http://doc.aldebaran.com/libqi/api/python/bind.html

# require imports
import qi
import time
from naoqi import ALProxy


# creating a class
class THWildQRCodeReader(object):

    def __init__(self, application):
        # wait 5 seconds for naoqi to be ready when running in autoload.ini
        time.sleep(5)
        # add app and session
        self.application = application
        self.session = application.session
        self.service_name = self.__class__.__name__  # getting service name

        # Getting a logger. Logs will be in /var/log/naoqi/servicemanager/{application id}.{service name}
        self.logger = qi.Logger(self.service_name)

        # create services from ALMotion, ALMemory and ALBArcodeReader
        self.motionService = self.session.service("ALMotion")
        self.barcodeService = self.session.service("ALBarcodeReader")
        self.memoryService = self.session.service("ALMemory")

        # create new event to store BarcodeData
        self.memoryService.declareEvent("THWildQRCodeReader/BarcodeData")

        self.subscriber_barcode = None
        self.signal_barcodereader = None
        self.signal_id_barcodereader = None
        self.name_qr_code = "test_barcode"

        self.connect_event("BarcodeReader/BarcodeDetected")

        self.counter_barcode_detected = 0

        # init finished
        self.logger.info("Initialized!")

    @qi.nobind
    @qi.singleThreaded()
    def connect_event(self, eventname):
        """
        function to conect to event
        :param eventname: is the name from event
        :return: None
        """
        if eventname == "BarcodeReader/BarcodeDetected":
            # subscibe to event
            self.subscriber_barcode = self.memoryService.subscriber(eventname)
            self.signal_barcodereader = self.subscriber_barcode.signal
            self.signal_id_barcodereader = self.subscriber_barcode.signal.connect(self.on_barcode_detected)
            self.barcodeService.subscribe(self.name_qr_code)

    @qi.nobind
    @qi.singleThreaded()
    def on_barcode_detected(self, data):
        """
        function to use Data from barcode
        :param data: full data of barcode with position parameters
        :return: None
        """
        print("I saw a barcode")
        # get Barcode (QR) data and print
        print("The event data are: " + str(data[0][0]))
        self.counter_barcode_detected += 1
        if self.counter_barcode_detected == 1:
            # write ditectet Barcode Data in a event
            self.memoryService.raiseEvent("THWildQRCodeReader/BarcodeData", data[0][0])
            # disconnect Barcodereader event
            self.disconnect_listener(self.signal_barcodereader, self.signal_id_barcodereader)
            time.sleep(10)
            print("sleep finished")
            self.counter_barcode_detected = 0
            self.connect_event("BarcodeReader/BarcodeDetected")



    """
    no binding to NAOqi framework not visible in Monitor app
    when no @qi.nobind is writing the function is public!
    """
    @qi.nobind
    @qi.singleThreaded()
    def disconnect_listener(self, signal, signal_id):
        """
        function to disconnect a event
        :param signal: is the created signal from a subscribed event
        :param signal_id: is the id of the signal
        :return: None
        """
        self.logger.info("Disconnect signal-id: " + str(signal_id))
        try:
            signal.disconnect(signal_id)
        except ValueError:
            self.logger.error("Can't disconnect signal. Error.")

    @qi.nobind
    def start_app(self):
        # do something when the service starts
        print("Starting app...")
        self.logger.info("Started!")

    @qi.nobind
    def stop_app(self):
        # To be used if internal methods need to stop the service from inside.
        # external NAOqi scripts should use ALServiceManager.stopService if they need to stop it.
        self.logger.info("Stopping service...")
        self.application.stop()
        self.logger.info("Stopped!")

    @qi.nobind
    def cleanup(self):
        # called when your module is stopped
        self.logger.info("Cleaning...")
        #self.set_autonomous_life(False)
        self.motionService.rest()
        self.logger.info("Cleaned!")


if __name__ == "__main__":
    # with this you can run the script for tests on remote robots
    # run : python main.py --qi-url 123.123.123.123
    # app = qi.Application("tcp://127.0.0.1:9559")
    app = qi.Application(args=None, autoExit=False, url="tcp://127.0.0.1:9559", raw=False)
    # app = qi.Application(args=None, autoExit=False, url="tcp://10.110.0.199:9559", raw=False)
    # app = qi.Application(sys.argv)
    app.start()
    service_instance = THWildQRCodeReader(app)
    service_id = app.session.registerService(service_instance.service_name, service_instance)
    service_instance.start_app()
    app.run()
    service_instance.cleanup()
    app.session.unregisterService(service_id)
