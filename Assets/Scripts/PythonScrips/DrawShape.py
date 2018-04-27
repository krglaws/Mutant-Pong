from PIL import Image, ImageDraw
import sys

def main():

	# get coordinates from cmd arguments
	coords = get_xy_pairs(sys.argv)

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

	# save image
	img.save("paddle.png", "PNG")

def get_xy_pairs(args):

	if (len(args) % 2 == 0):
		raise Exception("Invalid number of arguments: "+str(len(args)-1)+". Must pass an even number.")

	coords = []

	i = 1
	while i < len(sys.argv):
		x = float(sys.argv[i])
		y = float(sys.argv[i+1])
		xypair = (x, y)
		if ((x > 100) or (x < 0) or (y > 100) or (y < 0)):
			raise Exception("Invalid coordinate value: "+str(xypair)+". x and y values must be => 0 and <= 100.")
		
		coords.append(xypair)
		i += 2

	return coords

if __name__ == "__main__":
	main()
