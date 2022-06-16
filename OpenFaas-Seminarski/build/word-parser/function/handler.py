def handle(req):
    words = str(req).split()
    return tuple(words)
