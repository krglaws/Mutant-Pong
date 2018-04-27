from PIL import Image, ImageDraw
import sys

def main():

	# new image name
	name = sys.argv[1]

	# get coordinates from cmd arguments
	coords = get_xy_pairs(sys.argv[2:])

	# create new image canvas with black background
	img = Image.new('RGBA', (100, 100), 0)
	draw = ImageDraw.Draw(img, 'RGBA')

	# draw white shape
	draw.polygon(coords, (255, 255, 255, 255))

	# make background transparent
	data = img.getdata()
	newData = []
	for pixel in data:
		# if pixel is black, make transparent 
	    if pixel[0] == 0 and pixel[1] == 0 and pixel[2] == 0:
	        newData.append((255, 255, 255, 0))
	    else:
	        newData.append(pixel)
	img.putdata(newData)

	# EDIT THIS PATH TO SUIT YOUR SYSTEM
	img.save("C:/Users/<your username>/Documents/UnityProjects/MutantPong/Assets/Resources/"+name+".png", "PNG")
	

def get_xy_pairs(args):

	# check if uneven number of coordinates received
	if (len(args) % 2 == 1):
		raise Exception("Invalid number of arguments: "+str(len(args))+". Must pass an even number. "+str(args))

	coords = []

	# extract coordiate pairs
	i = 0
	while i < len(args):
		x = float(args[i])
		y = float(args[i+1])
		xypair = (x, y)
		if ((x > 100) or (x < 0) or (y > 100) or (y < 0)):
			raise Exception("Invalid coordinate value: "+str(xypair)+". x and y values must be => 0 and <= 100.")
		
		coords.append(xypair)
		i += 2

	return coords

if __name__ == "__main__":
	main()
